using System.ComponentModel.DataAnnotations;

namespace Lab12Chumbirayco.Models;

public partial class Pet
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = null!;

    [Required, StringLength(40)]
    public string Species { get; set; } = null!;

    [Range(0, 100)]
    public int Age { get; set; }

    public int OwnerId { get; set; }

    public virtual Owner? Owner { get; set; }
}
