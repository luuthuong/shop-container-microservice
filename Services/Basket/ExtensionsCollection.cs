namespace Basket
{
    public static class ExtensionsCollection
    {
        public static (int httpPort, int gprcPort) GetDefinedPorts(this IConfiguration configuration)
        {
            var grpcPort = configuration.GetValue("GRPC_PORT", 5001);
            var port = configuration.GetValue("PORT", 80);
            return (port, grpcPort);
        }
        //This method gets called by the runtimes. Use this method to add services to the container.
        public static IServiceCollection ConfigGrpc(this IServiceCollection services)
        {
            services.AddGrpc(opt =>
            {
                opt.EnableDetailedErrors = true;
            });
            return services;
        }
        public static IServiceCollection ConfigHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
    }
}
