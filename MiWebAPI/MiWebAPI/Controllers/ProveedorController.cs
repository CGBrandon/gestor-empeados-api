using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorData? _ProveedorData;
        public ProveedorController(ProveedorData ProveedorData)
        {
            _ProveedorData = ProveedorData;
        }


        [HttpGet]
        public async Task<IActionResult> Seleccionar()
        {
            List<Proveedor> Lista = await _ProveedorData.Seleccionar();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> SeleccionarPorId(long id)
        {
            Proveedor objeto = await _ProveedorData.SeleccionarPorId(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proveedor objeto)
        {
            bool respuesta = await _ProveedorData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Proveedor objeto)
        {
            bool respuesta = await _ProveedorData.Actualizar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSucces = respuesta });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            bool respuesta = await _ProveedorData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
