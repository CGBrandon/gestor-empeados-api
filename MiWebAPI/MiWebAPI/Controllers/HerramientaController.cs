using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;


namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientaController : ControllerBase
    {
        private readonly HerramientaData _HerramientaData;
        public HerramientaController(HerramientaData HerramientaData)
        {
            _HerramientaData = HerramientaData;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarTodos()
        {
            List<Herramienta> Lista = await _HerramientaData.ConsultarTodos();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SeleccionarPorId(long id)
        {
            Herramienta objeto = await _HerramientaData.SeleccionarPorId(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] Herramienta objeto)
        {
            bool respuesta = await _HerramientaData.Registrar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Herramienta objeto)
        {
            bool respuesta = await _HerramientaData.Actualizar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DarDeBaja(long id)
        {
            bool respuesta = await _HerramientaData.DarDeBaja(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
