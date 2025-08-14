using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Prenda
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrendaID { get; set; }

        [Required]
        public float Precio { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int ColorID { get; set; }

        [ForeignKey("ColorID")]
        public virtual Color Color { get; set; }

        [Required]
        public int RubroID { get; set; }

        [ForeignKey("RubroID")]
        public virtual Rubro Rubro { get; set; }


        [Required]
        public int MaterialID { get; set; }

        [ForeignKey("MaterialID")]
        public virtual Material Material { get; set; }


        [Required]
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }

        public int? ImagenID { get; set; }

        [ForeignKey("ImagenID")]
        public virtual Imagen Imagen { get; set; }

        public int? EstampadoID { get; set; }

        [ForeignKey("EstampadoID")]
        public virtual Imagen Estampado { get; set; }

        [Required]
        public int PostID { get; set; }


    }
}
