using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestBancoTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly BancoBDDContext _context;

        public CuentasController(BancoBDDContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
        {
            return Ok(await _context.Cuentas.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Cuenta>> CreateCuenta([FromBody] Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCuentaById), new { id = cuenta.intCuentaId }, cuenta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> GetCuentaById(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null) return NotFound();
            return Ok(cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, [FromBody] Cuenta updatedCuenta)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null) return NotFound();

            cuenta.intNumeroCuenta = updatedCuenta.intNumeroCuenta;
            cuenta.strTipoCuenta = updatedCuenta.strTipoCuenta;
            cuenta.intSaldoInicial = updatedCuenta.intSaldoInicial;
            cuenta.blEstadoCuenta = updatedCuenta.blEstadoCuenta;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null) return NotFound();

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
