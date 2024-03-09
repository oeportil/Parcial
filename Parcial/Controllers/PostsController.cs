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


        [HttpGet]
        [Route("posts-ultimo-autor")]
         public IActionResult ListarUltimosPostsPorAutor(string nombreAutor)
         {
         var ultimosPostsPorAutor = (from post in _contex.Posts
                                    join autor in _contex.Autores on post.AutorId equals autor.Id
                                    where autor.Nombre == nombreAutor
                                    orderby post.FechaPublicacion descending
                                    select new
                                    {
                                        post.Id,
                                        post.Titulo,
                                        post.Contenido,
                                        post.FechaPublicacion,
                                        Autor = autor.Nombre
                                    }).Take(20).ToList();

              if (ultimosPostsPorAutor.Count == 0)
                 {
                      return NotFound("No se encontraron posts para el autor especificado.");
                 }

        return Ok(ultimosPostsPorAutor);
          }

        [HttpGet]
        [Route("posts-por-libro")]
        public IActionResult ListarPostsPorLibro(string tituloLibro)
        {
            var postsPorLibro = (from autorLibro in _contex.AutorLibros
                                 join libro in _contex.Libros on autorLibro.LibroId equals libro.Id
                                 join autor in _contex.Autores on autorLibro.AutorId equals autor.Id
                                 join post in _contex.Posts on autor.Id equals post.AutorId
                                 where libro.Titulo == tituloLibro
                                 select new
                                 {
                                     PostId = post.Id,
                                     PostTitulo = post.Titulo,
                                     PostContenido = post.Contenido,
                                     PostFechaPublicacion = post.FechaPublicacion,
                                     AutorNombre = autor.Nombre
                                 }).ToList();

            if (postsPorLibro.Count == 0)
            {
                return NotFound("No se encontraron posts para el libro especificado.");
            }

            return Ok(postsPorLibro);
        }

    }
}
