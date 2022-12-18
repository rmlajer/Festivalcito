using Festivalcito.Server.Models.ShiftRepositoryFolder;
using Festivalcito.Server.Models.ShiftAssignmentRepositoryFolder;
using Festivalcito.Server.Models.AreaRepositoryFolder;
using Festivalcito.Server.Models.LoginCredentialRepositoryFolder;
using Festivalcito.Server.Models.PersonRepositoryFolder;
using Microsoft.AspNetCore.ResponseCompression;
using Festivalcito.Server.Models.PersonAssignmentRepositoryFolder;
using Festivalcito.Server.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();
builder.Services.AddScoped<IPersonAssignmentRepository, PersonAssignmentRepository>();
builder.Services.AddScoped<ILoginCredentialRepository, LoginCredentialRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
