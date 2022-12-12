using Festivalcito.Client;
using Festivalcito.Client.Services.ShiftServicesFolder;
using Festivalcito.Client.Services.ShiftAssignedServicesFolder;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.AreaServicesFolder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IShiftService, ShiftService>(client =>{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddHttpClient<IPersonService, PersonService>(client => {
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddHttpClient<IAreaService, AreaService>(client => {
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddHttpClient<IShiftAssignedService, ShiftAssignedService>(client => {
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});


await builder.Build().RunAsync();

