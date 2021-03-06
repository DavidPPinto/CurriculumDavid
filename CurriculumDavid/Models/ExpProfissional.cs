﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Models
{
    public class ExpProfissional
    {
        public int ExpProfissionalId { get; set; }
        
        [Required]
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required]
        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        public  DateTime DataFim { get; set; }
      
        [Required]
        [StringLength(300)]
        [Display(Name = "Nome da Empresa")]
        public string NomeEmpresa { get; set; }
        
        [Required]
        [StringLength(300)]
        [Display(Name = "Função")]
        public string Funcao { get; set; }
        public int DadosPessoaisId { get; set; }
        public DadosPessoais DadosPessoais { get; set; }


    }
}
