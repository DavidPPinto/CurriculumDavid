using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Models
{
    public class ComLing
    {
        [Key]
        public int CompetenciasId { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Língua")]

        public string Lingua { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Comprensão Oral")]
        public string CompreensaoOral { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Leitura")]
        public string Leitura { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Produção Oral")]
        public string ProducaoOral { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Interação Oral")]
        public string InteracaoOral { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Escrita")]
        public string Escrita { get; set; }
        
        public int DadosPessoaisId { get; set; }
        public DadosPessoais DadosPessoais { get; set; }

    }
}
