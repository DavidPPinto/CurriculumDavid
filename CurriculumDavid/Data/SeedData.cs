using CurriculumDavid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Data
{
    public class SeedData
    {
        internal static void PreencheDadosPessoais(CurriculumBdContext bd)
        {
            InsereDadosFicticios(bd);
        }

        private static void InsereDadosFicticios(CurriculumBdContext bd)
        {
            if (bd.DadosPessoais.Any()) return;

            bd.DadosPessoais.AddRange(new DadosPessoais[] {
                new DadosPessoais
                {
                    Nome = "João",
                    Morada = "Travessa",
                    Telefone = "912345678",
                    Email = "fe@g.pt"
                },
                new DadosPessoais
                {
                    Nome = "Manuel",
                    Morada = "Lugar",
                    Telefone = "912345678",
                    Email = "gt@g.pt"
                },
                new DadosPessoais
                {
                    Nome = "Maria",
                    Morada = "Travessa",
                    Telefone = "912345678",
                    Email = "fe@g.pt"
                }
            });

            bd.SaveChanges();
        }
    }
}
