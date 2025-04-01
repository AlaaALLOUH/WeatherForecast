using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using WeatherForecast.Services;

var builder = WebApplication.CreateBuilder(args);

// المصادقة
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// خدمة API للطقس
builder.Services.AddHttpClient<WeatherService>();

// Controllers + Razor Pages
builder.Services.AddControllers();
builder.Services.AddRazorPages(); // 🔥 لازم

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles(); // 🔥 لعرض ملفات HTML/CSS/JS
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 🔥 ربط الـ API
app.MapControllers();

// 🔥 ربط الواجهة (الرازر)
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();
