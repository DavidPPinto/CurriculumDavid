using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Models
{
    public class ListaDadosViewModel
    {
        public List<DadosPessoais> DadosPessoais { get; set; }

        public List<EduFor> EduFor { get; set; }

        public List<ExpProfissional> ExpProfissional { get; set; }

        public List<ComLing> ComLing { get; set; }

        public string NomePesquisar { get; set; }


        public Paginacao Paginacao { get; set; }
    }
}
