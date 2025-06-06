using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Repositories;
using HotelBookingSystem.Services.Hotel;
using HotelBookingSystem.Services.RoomService;
using HotelBookingSystem.Services.TextFileLogger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// 設定分散式快取，使用 SQL Server 儲存 Session
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("HotelBookingConnection");
    options.SchemaName = "dbo";
    options.TableName = "SessionData";
});

// 啟用 Session，設定 Cookie 名稱與過期時間
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".HotelBooking.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDistributedMemoryCache();

// 設定 DbContext 並使用 SQL Server
builder.Services.AddDbContext<HotelBookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingConnection"),
    sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(); // 啟用暫時性錯誤重試機制
    }));

builder.Services.AddScoped(typeof(IHotelBookingRepository<,>), typeof(HotelBookingRepository<,>));
builder.Services.AddScoped<IMemberRepository, MemberService>();
builder.Services.AddScoped<IRoomRepository, RoomService>();


// 註冊 AutoMapper 並載入 MappingProfile
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var logFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
var logFilePath = Path.Combine(logFolder, "app_log.txt");

// 確保資料夾存在
if (!Directory.Exists(logFolder))
{
    Directory.CreateDirectory(logFolder);
}

builder.Services.AddSingleton<ITextFileLogger>(provider => new TextFileLogger(logFilePath));

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
