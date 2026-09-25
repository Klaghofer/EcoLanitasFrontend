using EcoLanitas.web.Data;
using EcoLanitas.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLanitas.web.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _db;

    public ProductController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Product  (visible para todos, clientes incluidos)
    public async Task<IActionResult> Index()
    {
        var products = await _db.Products
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        return View(products);
    }

    // GET: /Product/Create
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View(new ProductFormViewModel());
    }

    // POST: /Product/Create
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        try
        {
            var product = new Product
            {
                Name = form.Name,
                Description = form.Description,
                Price = form.Price,
                Stock = form.Stock,
                Active = form.Active,
                ImageUrl = form.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Producto creado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"No se pudo guardar el producto: {ex.Message}";
            return View(form);
        }
    }

    // GET: /Product/Edit/5
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    // POST: /Product/Edit/5
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, Product product)
    {
        if (id != product.Id) return BadRequest();

        try
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Producto actualizado.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"No se pudo actualizar el producto: {ex.Message}";
            return View(product);
        }
    }

    // POST: /Product/Delete/5
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
