using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Models
{
    public class EduFor
    {
        public int EduForId { get; set; }

        [Required]
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        public  DateTime DataInicio { get; set; }

        [Required]
        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Nome da Formação")]
        public string NomeFormacao { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Entidade Formadora")]
        public string EntFormadora { get; set; }
    }
}
