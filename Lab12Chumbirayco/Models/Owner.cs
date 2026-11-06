using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab12Chumbirayco.Models;

public partial class Owner
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = null!;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = null!;

    [Required, StringLength(25)]
    public string Phone { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
