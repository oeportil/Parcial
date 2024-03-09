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
        [Route("GetAll")]

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

    }

}
