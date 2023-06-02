using System.Collections.Immutable;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Leave_Management.Web.Data;
using AutoMapper;
using Leave_Management.Web.Configurations;
using Leave_Management.Web.Contracts;
using Leave_Management.Web.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Leave_Management.Leave_Management.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(connectionString));


    
   builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>();

    // .AddEntityFrameworkStores<DbContext>();

//     builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
// {
//     //option settings...
// })
// .AddEntityFrameworkStores<IdentityDataContext>().AddDefaultTokenProviders();

    

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<IEmailSender, EmailSender>(); 
builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


app.Run();
