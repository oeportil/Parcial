using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_1A.Models;

namespace PARCIAL_1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Context _contex;
        public PostsController(Context context)
        {
            _contex = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Posts> listadopost = (from l in _contex.Posts
                                            select l).ToList();

            if (listadopost.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadopost);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult guardarPost([FromBody] Posts post)
        {
            try
            {
                _contex.Posts.Add(post);
                _contex.SaveChanges();
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarPost(int id, [FromBody] Posts postModificar)
        {
            Posts? postActual = _contex.Posts.FirstOrDefault(p => p.Id == id);

            if (postActual == null)
            {
                return NotFound();
            }

          
            postActual.Titulo = postModificar.Titulo;
            postActual.Contenido = postModificar.Contenido;
            postActual.FechaPublicacion = postModificar.FechaPublicacion;
            postActual.AutorId = postModificar.AutorId; 

            _contex.Entry(postActual).State = EntityState.Modified;
            _contex.SaveChanges();

            return Ok(postModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarPost(int id)
        {
            Posts? post = (from p in _contex.Posts
                          where p.Id == id
                          select p).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            _contex.Posts.Remove(post);
            _contex.SaveChanges();

            return Ok(post);
        }


    }
}
