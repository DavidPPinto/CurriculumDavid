using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CurriculumDavid.Models
{
    public class Informacao
    {
        public int InformacaoId { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFormacao { get; set; }
    }
}
