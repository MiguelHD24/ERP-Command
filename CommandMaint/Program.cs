using CommandMaint.Components;
using CommandMaint.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;
using SIM.Services;
using Utility;
using CommandMaint.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();

//Servicios
builder.Services.AddSingleton<ISweeAlert, SweetAlert>();
builder.Services.AddSingleton<ISHA256, SHA256>();
builder.Services.AddSingleton<IEncryption, Encryption>();
builder.Services.AddSingleton<IPasswordUtility, PasswordUtility>();
builder.Services.AddSingleton<IGeneral, General>();
builder.Services.AddScoped<IAuthentication, ServicesAuthentication>();

// Entity Framework
builder.Services.AddDbContext<CommandContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
