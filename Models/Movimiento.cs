using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Movimiento
    {
       public DateTime dtimeFecha { get; set; }
       public string strTipoMovimiento { get; set; }

       public int intValor { get; set; }

       public int intSaldo { get; set; }

       public int intMovimientoId { get;}


    }
}
