using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PcPartPicker.Areas.Identity.IdentityHostingStartup))]
namespace PcPartPicker.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}