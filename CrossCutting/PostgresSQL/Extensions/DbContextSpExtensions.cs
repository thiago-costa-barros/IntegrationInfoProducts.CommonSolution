using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System.Data;

namespace CommonSolution.CrossCutting.PostgresSQL.Extensions
{
    public static class DbContextSpExtensions
    {
        public static NpgsqlCommand FunctionCommand(this DbContext ctx, string schemaName, string functionName, params (string name, object? value)[] parameters)
        {
            var conn = (NpgsqlConnection)ctx.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            var paramPlaceholders = parameters.Length > 0
                ? string.Join(", ", parameters.Select(p => NormalizeParamName(p.name)))
                : string.Empty;

            cmd.CommandText = parameters.Length > 0
                ? $"SELECT * FROM \"{schemaName}\".\"{functionName}\"({paramPlaceholders})"
                : $"SELECT * FROM \"{schemaName}\".\"{functionName}\"()";

            foreach (var p in parameters)
                cmd.Parameters.Add(CreateParameter(p));

            var tx = ctx.Database.CurrentTransaction;
            if (tx is not null)
                cmd.Transaction = (NpgsqlTransaction)tx.GetDbTransaction();

            return cmd;
        }

        public static NpgsqlCommand StoredProcedureCommand(this DbContext ctx, string schemaName, string procName, params (string name, object? value)[] parameters)
        {
            var conn = (NpgsqlConnection)ctx.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = $"\"{schemaName}\".\"{procName}\"";

            foreach (var p in parameters)
                cmd.Parameters.Add(CreateParameter(p));

            var tx = ctx.Database.CurrentTransaction;
            if (tx is not null)
                cmd.Transaction = (NpgsqlTransaction)tx.GetDbTransaction();

            return cmd;
        }

        private static string NormalizeParamName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name cannot be null or empty", nameof(name));

            return name.StartsWith("@") ? name : "@" + name;
        }

        private static NpgsqlParameter CreateParameter((string name, object? value) p)
        {
            var paramName = NormalizeParamName(p.name);
            var value = p.value;

            if (value is Enum e)
                value = Convert.ToInt32(e);

            if (value is bool b)
                value = b ? 1 : 0;

            return new NpgsqlParameter(paramName, value ?? DBNull.Value);
        }
    }
}
