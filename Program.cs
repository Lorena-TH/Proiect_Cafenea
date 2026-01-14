using Microsoft.EntityFrameworkCore;
using Proiect_Cafenea.Data;
using Proiect_Cafenea.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CafeneaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IRecomandareService, RecomandareService>(client =>
{
    
    client.BaseAddress = new Uri("https://localhost:60805");
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
