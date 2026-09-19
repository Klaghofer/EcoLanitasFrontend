using EcoLanitas.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace EcoLanitas.web.Controllers;

public class ProductController : Controller
{
    private readonly Supabase.Client _supabase;

    public ProductController(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _supabase
            .From<Product>()
            .Get();
        var products = result.Models;
        return View(products);
    }

    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        return View();
    }
}