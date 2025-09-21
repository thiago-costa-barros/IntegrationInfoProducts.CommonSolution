namespace CommonSolution.Entities.Common
{
    public class AppSettings
    {
        public bool UseCache { get; set; }
        public bool UpdateRealtimeCache { get; set; }
        public int CacheExpirationInMinutes { get; set; }
    }
}
