using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoLanitas.web.Models;

[Table("User")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("role")]
    public string Role { get; set; } = string.Empty;
}
