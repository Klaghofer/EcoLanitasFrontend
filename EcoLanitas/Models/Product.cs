

using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EcoLanitas.web.Models;

[Supabase.Postgrest.Attributes.Table("Product")]
public class Product : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("stock")]
    public short Stock { get; set; }
    
    [Column("active")]
    public bool Active { get; set; }
    
    [Column("image_url")]
    public string? ImageUrl { get; set; }
}