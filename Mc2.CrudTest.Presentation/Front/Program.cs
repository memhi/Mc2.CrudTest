using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Mc2.CrudTest.Presentation.Front.Data.ViewServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Presentation.Front
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSweetAlert2(options => {
                options.Theme = SweetAlertTheme.Dark;
            });

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });//builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ICustomerManagementService, CustomerManagementService>();

            

            await builder.Build().RunAsync();
        }
    }
}
