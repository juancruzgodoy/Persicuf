using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class PrendaDTO
    {
        public float Precio {  get; set; }
        public int RubroID { get; set; }
        public int ColorID { get;set; }
        public int? ImagenID { get; set; }
        public int MaterialID { get; set; }
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public int? EstampadoID { get; set; }

        public int PostID {  get; set; }

    }
    public class PrendaDTOconID : PrendaDTO 
    { 
    public int ID { get; set; }
    }
}
