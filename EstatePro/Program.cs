using EstatePro.Data;
using EstatePro.Repositories;
using EstatePro.Repository;
using EstatePro.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>
    (
        options => options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("dbconn")
        )
    );
builder.Services.AddScoped<LogRegRepo, LogRegService>();
builder.Services.AddScoped<LeaseRepo, LeaseService>();
builder.Services.AddScoped<LeaseTenantRepo, LeaseTenantService>();
builder.Services.AddScoped<DashboardRepo, DashboardService>();
builder.Services.AddScoped<UserDashboardRepo, UserDashboardService>();
builder.Services.AddScoped<ReviewRepo, ReviewService>();
builder.Services.AddScoped<IPropertyRepository, PropertyService>();
builder.Services.AddScoped<AppointmentRepository>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<IBookingRepository, BookingService>();
builder.Services.AddScoped<ITransactionRepository, TransactionService>();
builder.Services.AddScoped<ReviewRepo, ReviewService>();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
