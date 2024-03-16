
using Serilog;

namespace Blessmate.Extensions
{
    public static class ExtensionsServices
    {
        public static WebApplicationBuilder AddSerilogExtension(this WebApplicationBuilder builder){

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
            
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

           return builder;
        }
    }
}