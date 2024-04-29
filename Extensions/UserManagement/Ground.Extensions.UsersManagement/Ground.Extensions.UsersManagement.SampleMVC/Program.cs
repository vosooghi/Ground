using Ground.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
    {
        builder.Configuration.GetSection("Identity").Bind(options);
    });

builder.Services.AddHttpContextAccessor();

//Ground
builder.Services.AddGroundWebUserInfoService(c =>
{
    c.DefaultUserIdClaim = "sid";
    c.DefaultUserNameClaim = "preferred_username";
});
//Type.1
//builder.Services.AddGroundWebUserInfoService(configuration);
//Type.2
//builder.Services.AddGroundWebUserInfoService(c =>
//{    
//    c.DefaultUserId = "1";
//});

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
