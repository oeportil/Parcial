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
            List<AutorLibros> listadoAutorlibros = (from l in _contex.AutorLibros
                                          select l).ToList();

            if (listadoAutorlibros.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutorlibros);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult guardarAutorLibro([FromBody] AutorLibros libro)
        {
            try
            {
                _contex.AutorLibros.Add(libro);
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

        public IActionResult actualizarAutorLibro(int id, [FromBody] AutorLibros libroModificar)
        {
            AutorLibros? libroActual = (from l in _contex.AutorLibros
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
            AutorLibros? libro = (from l in _contex.AutorLibros
                             where l.AutorId == id
                             select l).FirstOrDefault();

            if (libro == null)
            { return NotFound(); }

            _contex.AutorLibros.Attach(libro);
            _contex.AutorLibros.Remove(libro);
            _contex.SaveChanges();

            return Ok(libro);
        }


    }
}
