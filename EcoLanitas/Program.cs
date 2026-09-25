using EcoLanitas.web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Supabase Auth (solo para login/signup, ya NO para leer/escribir tablas) ---
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];

if (string.IsNullOrWhiteSpace(supabaseUrl) || string.IsNullOrWhiteSpace(supabaseKey))
{
    throw new InvalidOperationException(
        "Faltan las credenciales de Supabase en appsettings.json (Supabase:Url / Supabase:Key)");
}

var supabaseOptions = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = false
};
var supabase = new Supabase.Client(supabaseUrl, supabaseKey, supabaseOptions);
await supabase.InitializeAsync();
builder.Services.AddSingleton(supabase);

// --- Entity Framework Core + PostgreSQL (para todas las tablas: Product, User, etc.) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Falta la cadena de conexión 'DefaultConnection' en appsettings.json");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- MVC + Auth por cookies ---
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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
