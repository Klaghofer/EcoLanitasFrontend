using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace EcoLanitas.web.Controllers;
using EcoLanitas.web.Models;
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}









    
