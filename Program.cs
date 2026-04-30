using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Udemy.DatingApp.Web.Data;
using Udemy.DatingApp.Web.Interfaces;
using Udemy.DatingApp.Web.Services;

// 建立 WebApplicationBuilder，負責讀取設定、註冊服務與建立應用程式
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 註冊 MVC / Web API 控制器，讓專案可以使用 Controller 處理 HTTP 請求
builder.Services.AddControllers();
// 註冊 EF Core 的 AppDbContext，並指定使用 SQLite 當作資料庫
builder.Services.AddDbContext<AppDbContext>(opt =>
{
   // 從 appsettings 內的 ConnectionStrings:DefaultConnection 讀取連線字串
   opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

// 註冊 CORS 服務，允許前端跨來源呼叫此 API
builder.Services.AddCors();
// 註冊 TokenService 到 DI 容器，供登入或簽發 JWT 時使用
builder.Services.AddScoped<ITokenService, TokenService>();
// 設定 API 使用 JWT Bearer 驗證機制
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option =>
{
   // 從設定檔讀取簽章金鑰；若未設定，啟動時直接丟出例外避免執行到不安全狀態
   var tokenKey = builder.Configuration["TokenKey"] ?? throw new InvalidOperationException("Token key is not configured. - Program.cs");
   
   // 設定收到 JWT 時要如何驗證
   option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
   {
      // 驗證簽章是否正確（確認 Token 未被竄改）
       ValidateIssuerSigningKey = true,
      // 使用與簽發 Token 相同的對稱式金鑰來驗證簽章
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
      // 目前不驗證發行者（Issuer）
       ValidateIssuer = false,
      // 目前不驗證受眾（Audience）
       ValidateAudience = false

   };
});
    

// 根據前面註冊的服務與設定，建立實際可執行的 Web 應用程式
var app = builder.Build();

// 設定 CORS 規則，允許 Angular 開發伺服器從 localhost:4200 存取 API
app.UseCors(f => f.AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithOrigins("https://localhost:4200", "http://localhost:4200"));

// 啟用驗證中介軟體，解析並驗證 Request 內的 JWT
app.UseAuthentication();
// 啟用授權中介軟體，依據 [Authorize] 等屬性判斷是否允許存取
app.UseAuthorization();

// 將 Controller 路由對應到 HTTP 端點
app.MapControllers();

// 啟動應用程式並開始接收 HTTP 請求
app.Run();
