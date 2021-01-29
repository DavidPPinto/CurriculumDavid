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
            InsereDadosPessoaisFicticiosParaTestarPaginacao(bd);

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
        private static void InsereDadosPessoaisFicticiosParaTestarPaginacao(CurriculumBdContext bd)
        {


            for (int i = 0; i < 1000; i++)
            {
                bd.DadosPessoais.Add(new DadosPessoais
                {
                    Nome = "Produto " + i,
                    Morada = "teste" + i,
                    Telefone = "918547412",
                    Email = "f@f.pt"
                });
            }

            bd.SaveChanges();
        }
    }

}

