using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestBancoTest.Data;
using ApiRestBancoTest.Util;


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
        public async Task<ActionResult<MovimientoId>> CreateMovimiento([FromBody] MovimientoId movimiento)
        {
            try
            {
                string control = ControlesExtraUtil.Control_Data(movimiento);
                if (!String.IsNullOrEmpty(control))
                {
                    return BadRequest(control);
                }

                Movimiento movimiento_remap = new Movimiento();
                movimiento_remap.Fecha = DateTime.Now; //actualizo fecha de transaccion 
                movimiento_remap.intCuentaId = movimiento.intCuentaId;
                movimiento_remap.TipoMovimiento = movimiento.TipoMovimiento;


                var cuenta = await _context.Cuentas.FindAsync(movimiento.intCuentaId);
                if (cuenta == null) return NotFound("Error, la cuenta no fue encontrada");

                if (movimiento.TipoMovimiento == "Retiro" && cuenta.SaldoInicial < Math.Abs(movimiento.Valor))
                {
                    return BadRequest("Error, falta de saldo");
                }

                if (movimiento.TipoMovimiento == "Deposito")
                {
                    cuenta.SaldoInicial += Math.Abs(movimiento.Valor);//actualizo el saldo en tabla cuentas
                }
                else if (movimiento.TipoMovimiento == "Retiro")
                {
                    cuenta.SaldoInicial -= Math.Abs(movimiento.Valor);//actualizo el saldo en tabla cuentas
                }

                movimiento_remap.Valor = Math.Abs(movimiento.Valor);
                movimiento_remap.Saldo = cuenta.SaldoInicial;

                _context.Cuentas.Update(cuenta);
                _context.Movimientos.Add(movimiento_remap);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMovimientoById), new { id = movimiento.intMovimientoId }, movimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar el movimiento", details = ex.Message });
            }

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


        [HttpGet("{idCliente}/{InicioMes}/{FinMes}")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientosByIdDate(int idCliente, string InicioMes, string FinMes)
        {
            try
            {
                string control = ControlesExtraUtil.Control_Data_Cuenta( idCliente,  InicioMes,  FinMes);
                if (!String.IsNullOrEmpty(control))
                {
                    return BadRequest(control);
                }
                var cuentas = await _context.Cuentas
                .Where(c => c.intClienteId == idCliente)
                .ToListAsync();

                if (!cuentas.Any())
                {
                    return NotFound(new { message = "No se encontraron cuentas para el cliente especificado" });
                }

                var cliente = await _context.Clientes
                .Where(c => c.intClienteId == idCliente)
                .ToListAsync();
                var cliente_nombre = cliente.Select(c => c.Nombres).FirstOrDefault();
                var cuentaIdsListado = cuentas.Select(c => c.intCuentaId).ToList();  //inter

                DateTime.TryParse(InicioMes, out DateTime InicioMes_mod);
                DateTime.TryParse(FinMes, out DateTime FinMes_mod);

                var movimientos = await _context.Movimientos
                    .Where(m => cuentaIdsListado.Contains(m.intCuentaId) && m.Fecha >= InicioMes_mod && m.Fecha <= FinMes_mod)
                    .ToListAsync();

                if (!movimientos.Any())
                {
                    return NotFound(new { message = "No se encontraron movimientos para las cuentas del cliente en el rango de fechas proporcionado" });
                }

                var cuentaMap = cuentas.ToDictionary(c => c.intCuentaId, c => c.NumeroCuenta);

                var movimientosAgrupados = movimientos
                    .GroupBy(b => b.intCuentaId)
                    .Select(g => new
                    {
                        CuentaId = cuentaMap.TryGetValue(g.Key, out var numeroCuenta) ? numeroCuenta : "Cuenta no encontrada",
                        movimientos = g.Select(m => new
                        {
                            m.Fecha,
                            CuentaId = cuentaMap.TryGetValue(m.intCuentaId, out var numeroCuentaIndividual) ? numeroCuentaIndividual : "Cuenta no encontrada",
                            m.TipoMovimiento,
                            m.Valor,
                            m.Saldo,
                            m.intMovimientoId
                        }).ToList()
                    }).ToList();

                return Ok(new
                {
                    status = $"Reporte de cliente {cliente_nombre}",
                    message = "Movimientos encontrados.",
                    data = movimientosAgrupados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar el movimiento", details = ex.Message });
            }

        }
    }
}
