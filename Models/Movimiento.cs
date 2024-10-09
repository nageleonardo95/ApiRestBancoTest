using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Movimiento 
    {
       public DateTime Fecha { get; set; }

       public int intCuentaId { get; set; }

       public string TipoMovimiento { get; set; }

       public decimal Valor { get; set; }

       public decimal Saldo { get; set; }

       public int intMovimientoId { get;}


    }
}
