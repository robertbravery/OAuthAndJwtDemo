using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebOAuthDemo.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config=>
    {
        config.Cookie.Name = "oauth.cookie";
    });
builder.Services.AddRazorPages();

builder.Services.AddDbContext<WebOAuthDemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebOAuthDemoContext") ?? throw new InvalidOperationException("Connection string 'WebOAuthDemoContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
