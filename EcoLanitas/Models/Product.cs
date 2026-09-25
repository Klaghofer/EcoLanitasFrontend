using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoLanitas.web.Models;

[Table("Product")]
public class Product
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("stock")]
    public short Stock { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }
}
