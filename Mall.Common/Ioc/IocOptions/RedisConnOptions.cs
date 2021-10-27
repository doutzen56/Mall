namespace Mall.Common.Ioc.IocOptions
{
    public class RedisConnOptions
	{
		public string Host { get; set; }
		public int DB { get; set; } = 0;
		public int Prot { get; set; }
	}
}
