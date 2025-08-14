using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Color
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorID { get; set; }

        [Required]
        public string ColorNombre { get; set; } = string.Empty;

        [Required]
        public string CodigoHexa { get; set; } = string.Empty;
    }
}
