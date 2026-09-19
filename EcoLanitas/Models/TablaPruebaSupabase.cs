using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EcoLanitas.web.Models;

[Table("Prueba")]
public class Prueba : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }
    [Column ("nombre")]
    public string? Nombre { get; set; }
}