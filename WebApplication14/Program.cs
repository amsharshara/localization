using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.AddSupportedCultures(new string[] { "ar-SY", "en-US" })
    .AddSupportedUICultures(new string[] {"ar-SY","en-US"});
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddLocalization();

 var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context,next) =>
{
    if(context.Request.Query.Count() > 0 && 
    context.Request.Query["culture"].ToString()!="" )   
    {
        System.Threading.Thread.CurrentThread.CurrentCulture=
         System.Threading.Thread.CurrentThread.CurrentUICulture 
        =new CultureInfo(context.Request.Query["culture"].ToString());
        //save cuurrent culture in cookie
        context.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue
            (new RequestCulture(context.Request.Query["culture"].ToString()))
            , new CookieOptions() { Expires=DateTime.Now.AddYears(1)}
            );
         
    }

    await next.Invoke();
});

 app.UseRequestLocalization();//to support multi languages
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
 
app.UseAuthorization();

app.MapRazorPages();

app.Run();
