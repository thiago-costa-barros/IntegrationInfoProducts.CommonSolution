namespace CommonSolution.Entities.Common
{
    public class ConnectionStringOptions
    {
        public string PostgresConnection { get; set; } = string.Empty;
        public string MongoConnection { get; set; } = string.Empty;
        public string RedisConnection { get; set; } = string.Empty;
    }
}
