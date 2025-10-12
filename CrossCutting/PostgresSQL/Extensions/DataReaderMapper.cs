using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CommonSolution.CrossCutting.PostgresSQL.Extensions
{
    public static class DataReaderMapper
    {
        public static async Task<List<T>> MapToListAsync<T>(NpgsqlDataReader reader) where T : new()
        {
            var list = new List<T>();
            var colOrdinals = BuildOrdinalMap(reader);
            var props = GetMappableProperties(typeof(T));

            while (await reader.ReadAsync())
            {
                var item = new T();
                foreach (var prop in props)
                {
                    var colName = GetColumnName(prop);
                    if (!colOrdinals.TryGetValue(colName, out var ordinal)) continue;

                    var value = reader.GetValue(ordinal);
                    if (value is DBNull) { prop.SetValue(item, null); continue; }

                    prop.SetValue(item, ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
                }
                list.Add(item);
            }
            return list;
        }

        public static async Task<T?> MapToSingleAsync<T>(NpgsqlDataReader reader) where T : new()
        {
            var list = await MapToListAsync<T>(reader);
            return list.Count > 0 ? list[0] : default;
        }

        private static Dictionary<string, int> BuildOrdinalMap(NpgsqlDataReader reader)
        {
            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
                dict[reader.GetName(i)] = i;
            return dict;
        }

        private static PropertyInfo[] GetMappableProperties(Type t) =>
            t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
             .Where(p => p.CanWrite && p.GetSetMethod() is not null)
             .ToArray();

        private static string GetColumnName(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<ColumnAttribute>();
            return attr?.Name ?? prop.Name;
        }

        private static object? ChangeType(object value, Type target)
        {
            if (target.IsEnum) return Enum.ToObject(target, value);
            if (target == typeof(Guid)) return value is Guid g ? g : Guid.Parse(value.ToString()!);
            if (target == typeof(DateTime)) return Convert.ToDateTime(value);
            if (target == typeof(DateTimeOffset)) return value is DateTimeOffset d ? d : new DateTimeOffset(Convert.ToDateTime(value));
            if (target == typeof(bool)) return Convert.ToBoolean(value);
            if (target == typeof(int)) return Convert.ToInt32(value);
            if (target == typeof(long)) return Convert.ToInt64(value);
            if (target == typeof(decimal)) return Convert.ToDecimal(value);
            if (target == typeof(double)) return Convert.ToDouble(value);
            if (target == typeof(float)) return Convert.ToSingle(value);
            if (target == typeof(string)) return Convert.ToString(value);
            // Fallback
            return Convert.ChangeType(value, target);
        }
    }
}
