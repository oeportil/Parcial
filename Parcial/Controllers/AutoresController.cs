using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
