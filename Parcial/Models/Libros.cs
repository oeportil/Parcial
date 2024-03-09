using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL_1A.Models;

public partial class Libros
{
    [Key]
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;
}
