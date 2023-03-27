using InventoryManagementSystem.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MVCContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("EFCConnection"));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthorization();
app.UseAuthentication();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
