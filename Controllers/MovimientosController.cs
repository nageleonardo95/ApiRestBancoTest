using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestBancoTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly BancoBDDContext _context;

        public MovimientosController(BancoBDDContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Movimiento>> CreateMovimiento([FromBody] Movimiento movimiento)
        {
            var cuenta = await _context.Cuentas.FindAsync(movimiento.intMovimientoId);
            if (cuenta == null) return NotFound("Cuenta no encontrada.");

            if (movimiento.strTipoMovimiento == "Retiro" && cuenta.intSaldoInicial < movimiento.intValor)
            {
                return BadRequest("Saldo no disponible.");
            }

            movimiento.dtimeFecha = DateTime.Now;

            if (movimiento.strTipoMovimiento == "Deposito")
            {
                cuenta.intSaldoInicial += movimiento.intValor;
            }
            else if (movimiento.strTipoMovimiento == "Retiro")
            {
                cuenta.intSaldoInicial -= movimiento.intValor;
            }

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovimientoById), new { id = movimiento.intMovimientoId }, movimiento);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimientoById(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null) return NotFound();
            return Ok(movimiento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
        {
            return Ok(await _context.Movimientos.ToListAsync());
        }
    }
}
