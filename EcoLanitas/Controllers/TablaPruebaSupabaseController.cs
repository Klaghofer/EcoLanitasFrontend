using EcoLanitas.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoLanitas.web.Controllers;

public class PruebaController : Controller
{
    private readonly Supabase.Client _supabase;

    public PruebaController(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<IActionResult> Index()
    {
        var resultado = await _supabase
            .From<Prueba>()
            .Get();
        var pruebas = resultado.Models;
        return View(pruebas);
    }
}
