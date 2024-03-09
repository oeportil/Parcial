using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_1A.Models;

namespace PARCIAL_1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autorLibroController : ControllerBase
    {

        private readonly Context _contex;

        public autorLibroController(Context context)
        {
            _contex = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<AutorLibro> listadoAutorlibros = (from l in _contex.AutorLibro
                                          select l).ToList();

            if (listadoAutorlibros.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutorlibros);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult guardarAutorLibro([FromBody] AutorLibro libro)
        {
            try
            {
                _contex.AutorLibro.Add(libro);
                _contex.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarAutorLibro(int id, [FromBody] AutorLibro libroModificar)
        {
            AutorLibro? libroActual = (from l in _contex.AutorLibro
                                        where l.AutorId == id
                                        select l).FirstOrDefault();

            if (libroActual == null)
            { return NotFound(); }

            libroActual.LibroId = libroModificar.LibroId;
            libroActual.Orden = libroModificar.Orden;
            libroActual.AutorId = libroModificar.AutorId;

            _contex.Entry(libroActual).State = EntityState.Modified;
            _contex.SaveChanges();

            return Ok(libroModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult eliminarAutorLibro(int id)
        {
            AutorLibro? libro = (from l in _contex.AutorLibro
                             where l.AutorId == id
                             select l).FirstOrDefault();

            if (libro == null)
            { return NotFound(); }

            _contex.AutorLibro.Attach(libro);
            _contex.AutorLibro.Remove(libro);
            _contex.SaveChanges();

            return Ok(libro);
        }

        [HttpGet]
        [Route("BuscarLibrosPorAutor")]
        public IActionResult BuscarLibrosPorAutor(string nombreAutor)
        {
            if (string.IsNullOrWhiteSpace(nombreAutor))
            {
                return BadRequest("El nombre del autor no puede estar vacío");
            }

            // Obtener registros de AutorLibros que coinciden con el nombre del autor
            var autorLibro = _contex.AutorLibro
                .Include(al => al.Libro)
                .Include(al => al.Autor)
                .Where(al => al.Autor.Nombre.Contains(nombreAutor))
                .ToList();

            if (autorLibro.Count == 0)
            {
                return NotFound("No se encontraron libros para el autor proporcionado");
            }

            // Extraer los libros de los registros de AutorLibros encontrados
            var libros = autorLibro.Select(al => al.Libro).ToList();

            return Ok(libros);
        }

    }
}
