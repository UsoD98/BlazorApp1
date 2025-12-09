using BlazorApp1.Components;
using BlazorApp1.Data;
using BlazorApp1.Domain.Repository;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// IConfiguration은 자동으로 환경(appsettings.{Environment}.json + user-secrets + 환경변수)을 병합합니다.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
// Radzen 서비스 등록
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();
// DapperContext & Repository 등록
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 기본 로그 제공자 등록
builder.Logging.ClearProviders();
builder.Logging.AddConsole();       // 콘솔 로그
builder.Logging.AddDebug();         // 디버그 출력


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
