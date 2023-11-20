using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Food.Modelos
{
    public class TipoComida
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es Requerido")]
        [MaxLength(60, ErrorMessage="Nombre debe ser Maximo 60 Caracteres")]
        public string Nombre { get; set; }

    }
}
