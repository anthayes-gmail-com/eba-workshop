using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardLite.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Retain container-compatible URL binding from original Program.cs
builder.WebHost.UseUrls("http://0.0.0.0:80");

// ConfigureServices
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Initialize database on startup (was in Startup.Configure)
var dbInitializer = new DatabaseInitializer(app.Configuration);
dbInitializer.Initialize();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
