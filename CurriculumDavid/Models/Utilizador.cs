using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Models
{
    public class Utilizador
    {
        public int UtilizadorId { get; set; }

        [Required]
        [StringLength(200)]
        public String Nome { get; set; }

        [Required]
        [RegularExpression(@"(9[1236]|2\d)\d{7}", ErrorMessage = "Telefone Inválido")]
        [StringLength(9, MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

    }
}
