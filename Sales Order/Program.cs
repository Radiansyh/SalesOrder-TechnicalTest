using Microsoft.EntityFrameworkCore;
using SalesOrder.Infrastructure.Context;
using SalesOrder.Infrastructure.Repository;
using SalesOrder.Infrastructure.Repository.Customer;
using SalesOrder.Infrastructure.Repository.SalesOrder;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SalesOrderContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IComCustomerRepository, ComCustomerRepository>();
builder.Services.AddScoped<ISoOrderRepository, SoOrderRepository>();
builder.Services.AddScoped<ISoItemRepository, SoItemRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SalesOrder}/{action=Order}/{id?}");

app.Run();
