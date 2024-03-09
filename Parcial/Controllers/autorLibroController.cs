using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
