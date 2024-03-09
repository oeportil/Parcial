using System;
using System.Collections.Generic;

namespace PARCIAL_1A.Models;

public partial class Autores
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
}
