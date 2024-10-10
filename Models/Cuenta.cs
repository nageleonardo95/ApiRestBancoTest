using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestBancoTest.Models
{
    public class Cuenta  
    {
       public string NumeroCuenta { get; set; }
       public string TipoCuenta { get; set; }
       public decimal SaldoInicial { get; set; }
       public bool EstadoCuenta { get; set; }
        
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int intCuentaId { get; set; }

       
       public int intClienteId { get; set; }

       [ForeignKey("intClienteId")]
       public Cliente Cliente { get; set; }

    }
}
