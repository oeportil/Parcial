using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL_1A.Models;

public partial class Posts
{
    [Key]
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime FechaPublicacion { get; set; }

    public int? AutorId { get; set; }

    public virtual Autores? Autor { get; set; }
}
