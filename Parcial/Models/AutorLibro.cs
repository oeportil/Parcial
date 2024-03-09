using System;
using System.Collections.Generic;

namespace PARCIAL_1A.Models;

public partial class AutorLibro
{
    public int? AutorId { get; set; }

    public int? LibroId { get; set; }

    public int Orden { get; set; }

    public virtual Autore? Autor { get; set; }

    public virtual Libro? Libro { get; set; }
}
