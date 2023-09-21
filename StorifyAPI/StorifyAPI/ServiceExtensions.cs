namespace StorifyAPI
{
    public static class ServiceExtensions
    {
        public static string corsPolicy = "CorsPolicy";
        public static void ConfigCORS(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(corsPolicy, builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        public static void ConfigIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(option => { });
        }
    }
}
