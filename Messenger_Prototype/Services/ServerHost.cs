using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Messenger_Prototype.Hubs;

namespace Messenger_Prototype.Services
{
    public class ServerHost
    {
        private IHost host;

        public async Task StartAsync()
        {
            var builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(services =>
            {
                services.AddSignalR();
            });

            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:5000");
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/chatHub");
                    });
                });
            });

            host = builder.Build();

            await host.StartAsync();
        }

        public async Task StopAsync()
        {
            if(host != null)
            {
                await host.StopAsync();
                host.Dispose();
            }
        }
    }
}
