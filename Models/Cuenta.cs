using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Cuenta  
    {
       public string NumeroCuenta { get; set; }
       public string TipoCuenta { get; set; }
       public decimal SaldoInicial { get; set; }
       public bool EstadoCuenta { get; set; }
       public int intCuentaId { get; set; }
       public int intClienteId { get; set; }

    }
}
