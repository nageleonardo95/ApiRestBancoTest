using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestBancoTest.Data;

namespace ApiRestBancoTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly BancoBDDContext _context;

        public ClientesController(BancoBDDContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return Ok(await _context.Clientes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] dtoClienteIn dtoClienteIn)
        {
            try { 
                var cliente = new Cliente
                {
                    Nombres = dtoClienteIn.Nombres,
                    Genero = dtoClienteIn.Genero,
                    Edad = dtoClienteIn.Edad,
                    Identificacion = dtoClienteIn.Identificacion,
                    Direccion = dtoClienteIn.Direccion,
                    Contrasena = dtoClienteIn.Contrasena,
                    Telefono = dtoClienteIn.Telefono,
                    Estado = true
                };
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                //return CreatedAtAction(nameof(GetClienteById), new { id = cliente.intClienteId}, cliente);

                return Ok(new
                {
                    status = "200",
                    message = $"Ingreso de cliente exitoso: {cliente.intClienteId}",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al ingresar datos", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(new { message = $"Cliente con ID: {id} no fue encontrado" });
            }
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente updatedCliente)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            cliente.Nombres = updatedCliente.Nombres;
            cliente.Genero = updatedCliente.Genero;
            cliente.Edad = updatedCliente.Edad;
            cliente.Identificacion = updatedCliente.Identificacion;
            cliente.Direccion = updatedCliente.Direccion;
            cliente.Telefono = updatedCliente.Telefono;
            cliente.Contrasena = updatedCliente.Contrasena;
            cliente.Estado = updatedCliente.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(new { message = $"Cliente con ID: {id} no fue encontrado" });
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "200",
                message = $"Se borro con exito el clinete con id: {id}",
            });
        }
    }
}
