using System;
using System.Collections.Generic;

namespace PARCIAL_1A.Models;

public partial class Posts
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime FechaPublicacion { get; set; }

    public int? AutorId { get; set; }

    public virtual Autores? Autor { get; set; }
}
