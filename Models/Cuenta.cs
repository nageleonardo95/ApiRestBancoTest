using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Cuenta  
    {
       public int intNumeroCuenta { get; set; }
       public string strTipoCuenta { get; set; }
       public int intSaldoInicial { get; set; }
       public bool blEstadoCuenta { get; set; }
       public int intCuentaId { get; set; }
    }
}
