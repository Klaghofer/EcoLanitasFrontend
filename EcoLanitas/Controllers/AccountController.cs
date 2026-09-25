using System.Security.Claims;
using EcoLanitas.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLanitas.web.Controllers;

public class AccountController : Controller
{
    private readonly Supabase.Client _supabase;
    private readonly AppDbContext _db;

    public AccountController(Supabase.Client supabase, AppDbContext db)
    {
        _supabase = supabase;
        _db = db;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            // Autenticación: sigue usando Supabase Auth (esto SIEMPRE funcionó bien).
            var session = await _supabase.Auth.SignIn(email, password);

            if (session == null)
            {
                ViewBag.Error = "E-Mail o contraseña incorrectos.";
                return View();
            }

            var authUser = _supabase.Auth.CurrentUser;
            if (authUser == null || string.IsNullOrEmpty(authUser.Id))
            {
                ViewBag.Error = "No se pudo encontrar el usuario.";
                return View();
            }

            var userId = Guid.Parse(authUser.Id);

            // Datos del perfil: ahora se leen directo de Postgres con EF Core,
            // sin depender de RLS ni de tokens viajando entre capas.
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                ViewBag.Error = "No existe el perfil de este usuario.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authUser.Id),
                new Claim(ClaimTypes.Email, email ?? ""),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Role, user.Role ?? ""),
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookies", principal);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            ViewBag.Error = "No se pudo iniciar sesión.";
            return View();
        }
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
