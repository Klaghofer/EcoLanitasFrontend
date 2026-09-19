using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EcoLanitas.web.Controllers;

public class AccountController : Controller
{
    private readonly Supabase.Client _supabase;

    public AccountController(Supabase.Client supabase)
    {
        _supabase = supabase;
    }
    [HttpGet]
    // GET
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            // check email & password with supabase AUTH
            var session = await _supabase.Auth.SignIn(email, password);
            if (session == null)
            {
                ViewBag.Error = "E-Mail o contrasena incorrectos.";
                return View();
            }

            var authUser = _supabase.Auth.CurrentUser;
            if (authUser == null)
            {
                ViewBag.Error = " no se pudo encontrar el usuario";
                return View();
            }
            
            if (string.IsNullOrEmpty(authUser.Id))
            {
                ViewBag.Error = "usuario no tiene un id valido";
                return View();
            }
            var userId = Guid.Parse(authUser.Id);
            
            var result = await _supabase
                .From<EcoLanitas.web.Models.User>()
                .Where(x => x.Id == userId)
                .Get();
            var user = result.Models.FirstOrDefault();
            if (user == null)
            {
                ViewBag.Error = "no existe el perfil de este usuario";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authUser.Id),
                new Claim(ClaimTypes.Email, email ??""),
                new Claim(ClaimTypes.Name, user.Name  ??""),
                new Claim(ClaimTypes.Role, user.Role ??""),
            };

            var identity = new ClaimsIdentity(claims, "Cookies");

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookies", principal);
            return RedirectToAction("Index", "Home");
            
            
        }
        catch (Exception)
        {
            ViewBag.Error = "No se pudo iniciar sesión.";
            throw;
        }
    }
    //logout
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
