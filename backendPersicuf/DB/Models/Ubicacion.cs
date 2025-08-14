using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Ubicacion
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UbicacionID { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public float PosX { get; set; }

        [Required]
        public float PosY { get; set; }

    }
}
