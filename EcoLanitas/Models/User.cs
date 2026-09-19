using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EcoLanitas.web.Models;

[Supabase.Postgrest.Attributes.Table("User")]
public class User : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("name")] public string Name { get; set; } = string.Empty;

    [Column("role")] public string Role { get; set; } = string.Empty;

}