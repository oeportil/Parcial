using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_1A.Models;

namespace PARCIAL_1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly Context _contex;
        public AutoresController(Context context)
        {
            _contex = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Autores> listadoautores = (from l in _contex.Autores
                                          select l).ToList();

            if (listadoautores.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoautores);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult guardarAutor([FromBody] Autores autor)
        {
            try
            {
                _contex.Autores.Add(autor);
                _contex.SaveChanges();
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarLibro(int id, [FromBody] Autores autormodificar)
        {
            Autores? autorActual = (from l in _contex.Autores
                                   where l.Id == id
                                   select l).FirstOrDefault();

            if (autorActual == null)
            { return NotFound(); }

            autorActual.Nombre = autormodificar.Nombre;

            _contex.Entry(autorActual).State = EntityState.Modified;
            _contex.SaveChanges();

            return Ok(autormodificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult eliminarLibro(int id)
        {
                Autores? autor = (from l in _contex.Autores
                             where l.Id == id
                             select l).FirstOrDefault();

            if (autor == null)
            { return NotFound(); }

            _contex.Autores.Attach(autor);
            _contex.Autores.Remove(autor);
            _contex.SaveChanges();

            return Ok(autor);
        }

    }
}
