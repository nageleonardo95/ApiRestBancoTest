using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestBancoTest.Models
{
    public class Movimiento 
    {
       public DateTime Fecha { get; set; }
        public int intCuentaId { get; set; }

       public string TipoMovimiento { get; set; }

       public decimal Valor { get; set; }

       public decimal Saldo { get; set; }

       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int intMovimientoId { get;}

       [ForeignKey("intCuentaId")]
       public Cuenta Cuenta { get; set; }
    }
}
