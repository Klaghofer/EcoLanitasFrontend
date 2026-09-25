using System.ComponentModel.DataAnnotations;

namespace EcoLanitas.web.Models;

public class ProductFormViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser positivo")]
    public decimal Price { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "El stock debe ser positivo")]
    public short Stock { get; set; }

    public bool Active { get; set; } = true;

    public string? ImageUrl { get; set; }
}
