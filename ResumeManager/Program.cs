using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ResumeDbContext");
builder.Services.AddDbContext<ResumeDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicantDbContextcs>(options =>
       options.UseSqlServer(connectionString));

builder.Services.AddDbContext<OutDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("OutDbContextCS")));


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
