var builder = WebApplication.CreateBuilder(args);

// Supabase.

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];

Console.WriteLine($"URLENontrada: {!string.IsNullOrWhiteSpace(supabaseUrl)}");
Console.WriteLine($"KeyENontrada: {!string.IsNullOrWhiteSpace(supabaseKey)}");

if (string.IsNullOrWhiteSpace(supabaseUrl) ||
    string.IsNullOrWhiteSpace(supabaseKey))
{
    throw new InvalidOperationException(
        "Faltan las credenciales de Supabase en appsettings.json");
}

var supabase = new Supabase.Client(supabaseUrl, supabaseKey);
await supabase.InitializeAsync();
builder.Services.AddSingleton(supabase);

//MCV
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
    
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();