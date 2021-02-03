using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CurriculumDavid.Models
{
    public class DadosPessoais
    {
        public int DadosPessoaisId { get; set; }
        
        [Required]
        [StringLength(300)]
        public string Nome { get; set; }

        [Required]
        [StringLength(500)]
        public string Morada { get; set; }

        [Required]
        [RegularExpression(@"(9[1236]|2\d)\d{7}", ErrorMessage = "Telefone Inválido")]
        [StringLength(9, MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        public byte[] Foto { get; set; }

        public ICollection<ComLing> ComLings { get; set; }

        public ICollection<ExpProfissional> ExpProfissionals { get; set; }

        public ICollection<EduFor> EduFors { get; set; }
        public ICollection<DadosPessoais> DadosPessoai { get; set; }
        
    }
}
