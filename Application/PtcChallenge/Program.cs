using Data.Entities.Context;
using Microsoft.EntityFrameworkCore;
using PtcChallenge.Extensions.Injections;

var builder = WebApplication.CreateBuilder(args);

// Adding personal extensions to the container.
builder.Services
        .AddContext(builder.Configuration)
        .AddRepositories()
        .AddServices()
        .AddAutoMapper()
        .AddRabbitMq(builder.Configuration);

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var coreContext = serviceScope.ServiceProvider.GetRequiredService<ContextDB>();

    coreContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicle}/{action=Index}/{id?}");

app.Run();
