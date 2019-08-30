using Microsoft.AspNetCore.Builder;

namespace Lif.ApiBasic
{
    public static class LifAppBuilderExtensions
    {
        public static IApplicationBuilder UseLif(this IApplicationBuilder app)
        {
            Global.ServiceProvider = app.ApplicationServices;
            return app;
        }
    }
}
