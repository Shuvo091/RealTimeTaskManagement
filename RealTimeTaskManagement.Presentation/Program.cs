using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeTaskManagement.Data.Context;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Data.Repositories;
using RealTimeTaskManagement.Services;
using RealTimeTaskManagement.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<RealTimeTaskManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<UserEntity, IdentityRole>(
            options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RealTimeTaskManagementDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Add Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
