namespace ServerSide.Framework.Redis
{
    public class RedisConfig
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public int? Db { get; set; }
    }
}
