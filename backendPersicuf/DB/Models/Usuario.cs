using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Usuario
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contrasenia { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public int PermisoID { get; set; }

        [ForeignKey("PermisoID")]
        public virtual Permiso Permiso { get; set; }

    }
}
