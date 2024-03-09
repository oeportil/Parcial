using System;
using System.Collections.Generic;

namespace PARCIAL_1A.Models;

public partial class AutorLibros
{
    public int? AutorId { get; set; }

    public int? LibroId { get; set; }

    public int Orden { get; set; }

    public virtual Autores? Autor { get; set; }

    public virtual Libros? Libro { get; set; }
}
