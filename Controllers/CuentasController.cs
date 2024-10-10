using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestBancoTest.Data;
using System;
using ApiRestBancoTest.Util;


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
        public async Task<ActionResult<Cuenta>> CreateCuenta([FromBody] dtoCuentaIn dtoCuentaIn)
        {
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 1001);
            try
            {
                string control = ControlesExtraUtil.Control_Data_Cuenta_Generar(dtoCuentaIn);
                if (!String.IsNullOrEmpty(control))
                {
                    return BadRequest(control);
                }
                var cuenta = new Cuenta
                {
                    NumeroCuenta = (555.ToString() + dtoCuentaIn.intClienteId.ToString() + numeroAleatorio.ToString()), //solo por ejer
                    TipoCuenta = dtoCuentaIn.TipoCuenta,
                    SaldoInicial = 0,
                    EstadoCuenta = true,
                    intClienteId = dtoCuentaIn.intClienteId,
                };

                _context.Cuentas.Add(cuenta);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    status = "200",
                    message = $"Se genero con exito el numero de cuenta: {cuenta.NumeroCuenta} del cliente",
                });
            }
             catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al generar cuenta, revise existencia de cliente", details = ex.Message });
            }
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

            cuenta.NumeroCuenta = updatedCuenta.NumeroCuenta;
            cuenta.TipoCuenta = updatedCuenta.TipoCuenta;
            cuenta.SaldoInicial = updatedCuenta.SaldoInicial;
            cuenta.EstadoCuenta = updatedCuenta.EstadoCuenta;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound(new { message = $"Cuenta con ID: {id} no fue encontrado" });
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "200",
                message = $"Se borro con exito la cueta con id: {id}",
            });
        }
    }
}
