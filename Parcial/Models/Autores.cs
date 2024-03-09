using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL_1A.Models;

public partial class Autores
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
}
