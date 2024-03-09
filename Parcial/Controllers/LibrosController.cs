using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL_1A.Models;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL_1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly Context _contex;

        public LibrosController(Context context) 
        {
            _contex = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Libros> listadolibros = (from l in _contex.Libros
                                          select l).ToList();

            if(listadolibros.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadolibros);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult guardarLibro([FromBody] Libros libro)
        {
            try
            {
                _contex.Libros.Add(libro);
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

        public IActionResult actualizarLibro(int id, [FromBody] Libros libroModificar)
        {
            Libros? libroActual = (from l in _contex.Libros
                                      where l.Id == id
                                      select l).FirstOrDefault();

            if(libroActual == null)
            { return NotFound(); }

            libroActual.Titulo = libroModificar.Titulo;

            _contex.Entry(libroActual).State = EntityState.Modified;
            _contex.SaveChanges();

            return Ok(libroModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult eliminarLibro(int id)
        {
            Libros? libro = (from l in _contex.Libros
                                   where l.Id == id
                                   select l).FirstOrDefault();

            if (libro == null)
            { return NotFound(); }

            _contex.Libros.Attach(libro);
            _contex.Libros.Remove(libro);
            _contex.SaveChanges();

            return Ok(libro);
        }

    }

}
