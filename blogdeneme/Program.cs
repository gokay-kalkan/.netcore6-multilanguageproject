using blogdeneme.Data;
using blogdeneme.ResourceServices;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.GetSection("ResourceNames").Get<string[]>();

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews()
        .AddViewLocalization();



foreach (var resourceName in config)
{
    mvcBuilder.AddDataAnnotationsLocalization(options =>
    {
        var assemblyName = new AssemblyName(typeof(Program).GetTypeInfo().Assembly.FullName);
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(resourceName, assemblyName.Name);
    });
}


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCulture = new List<CultureInfo>
    {
        new CultureInfo("tr-TR"),
    new CultureInfo("en-US"),
    new CultureInfo("fr-FR"),



    };
    options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportCulture;
    options.SupportedUICultures = supportCulture;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

builder.Services.AddDbContext<DataContext>(conf=> conf.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddSingleton<ContactService>();
builder.Services.AddSingleton<PrivacyService>();
builder.Services.AddSingleton<MenuService>();

builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

builder.Services.AddSession();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
