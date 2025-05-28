using Elfie.Serialization;
using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

using System.Globalization;
using System.Security.Claims;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromMinutes(5));

builder.Services.AddRazorPages();

builder.Services.AddScoped<IHangHoaRepository, EFHangHoaRepository>();
builder.Services.AddScoped<ILoaiHangHoaRepository, EFLoaiHangHoaRepository>();
builder.Services.AddScoped<IKhoHangRepository, EFKhoHangRepository>();
builder.Services.AddScoped<ITonKhoRepository, EFTonKhoRepository>();
builder.Services.AddScoped<INhaCungCapRepository, EFNhaCungCapRepository>();
builder.Services.AddScoped<ILoaiKhachHangRepository, EFLoaiKhachHangRepository>();
builder.Services.AddScoped<INhanVienRepository, EFNhanVienRepository>();
builder.Services.AddScoped<IKhachHangRepository, EFKhachHangRepository>();
builder.Services.AddScoped<IDonXuatHangRepository, EFDonXuatHangRepository>();
builder.Services.AddScoped<IDonNhapHangRepository, EFDonNhapHangRepository>();
builder.Services.AddScoped<ICTDonNhapRepository, EFCTDonNhapRepository>();
builder.Services.AddScoped<ICTDonXuatRepository, EFCTDonXuatRepository>();
builder.Services.AddScoped<IPhieuKiemKeRepository, EFPhieuKiemKeRepository>();
builder.Services.AddScoped<ICTPhieuKiemKeRepository, EFCTPhieuKiemKeRepository>();
builder.Services.AddScoped<IExcelExportService, ExcelExportService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddGoogle(options =>
    {
        // Lấy thông tin cấu hình từ appsettings.json
        var googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];

        // Thêm các scope nếu cần
        options.Scope.Add("profile");
        options.Scope.Add("email");

        // Lấy thêm thông tin từ Google
        options.ClaimActions.MapJsonKey("picture", "picture", "url");
        options.ClaimActions.MapJsonKey("email_verified", "email_verified", "boolean");
    });

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
        options.Fields.Add("picture"); // Lấy ảnh đại diện
        options.Fields.Add("email");
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();




using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Tạo role "Admin" nếu chưa có
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var role = new IdentityRole("Admin");
        await roleManager.CreateAsync(role);
    }

    // Tạo role "User" nếu chưa có
    if (!await roleManager.RoleExistsAsync("User"))
    {
        var role = new IdentityRole("User");
        await roleManager.CreateAsync(role);
    }

    // Tạo tài khoản Admin mặc định nếu chưa có
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            FullName = "Administrator",
            AvatarUrl = "/images/avatars/default.png",
        };

        var result = await userManager.CreateAsync(user, "AdminPassword123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    // Đảm bảo các tài khoản mới đăng ký sẽ được gán role "User"
    var defaultUser = new ApplicationUser
    {
        UserName = "defaultuser@example.com",
        Email = "defaultuser@example.com",
        FullName = "Default User",
        AvatarUrl = "/images/avatars/default.png",
    };

    var defaultUserResult = await userManager.CreateAsync(defaultUser, "UserPassword123!");
    if (defaultUserResult.Succeeded)
    {
        await userManager.AddToRoleAsync(defaultUser, "User");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
