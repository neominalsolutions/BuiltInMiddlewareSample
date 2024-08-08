using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MVC i�in ekleyelim. AddViewLocalization().AddDataAnnotationsLocalization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Uygulama Bazl� Cookie Y�netimi Cookie Policy

builder.Services.Configure<CookiePolicyOptions>(opt =>
{
  opt.MinimumSameSitePolicy = SameSiteMode.Strict;
  opt.Secure = CookieSecurePolicy.Always;
  opt.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
  opt.OnAppendCookie = (action) =>
  {
    Console.WriteLine("Cookie eklendi");
  };
  opt.OnDeleteCookie = (action) =>
  {
    Console.WriteLine("Cookie silindi");
  };

});
// DI �zerindne IHttpContextAccessor kulland���m�zda register etmemiz gereken servis.
builder.Services.AddHttpContextAccessor();

// MVC de AddDistributedMemoryCache kullanmam�za gerek yok
// DistributedMemoryCache Redis ilede implemente edilebilir.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(config =>
{
  config.Cookie.Name = "TurkiyeId";
  config.Cookie.IsEssential = true;
  config.Cookie.HttpOnly = true;
  config.Cookie.MaxAge = TimeSpan.FromMinutes(20);
  config.Cookie.SameSite = SameSiteMode.Strict;
  config.IdleTimeout = TimeSpan.FromMinutes(20);

});

builder.Services.AddResponseCompression(opt =>
{
  // 
  opt.EnableForHttps = true;
  opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json", "application/xml", "text/json" });
  opt.Providers.Add<BrotliCompressionProvider>();
  opt.Providers.Add<GzipCompressionProvider>();

});

builder.Services.AddResponseCaching();




builder.Services.AddLocalization(opt =>
{
  opt.ResourcesPath = "Resources";

});

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
  var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("tr-TR") };

  opt.DefaultRequestCulture = new("tr-TR");
  opt.SupportedUICultures = supportedCultures;

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();

// Https Redirection Alt�nda olmas�na �zel g�sterelim.
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseResponseCompression();
app.UseResponseCaching();

app.MapControllers();

app.Run();
