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
            //InsereDadosPessoaisFicticios(bd);
            //InsereDadosFicticios(bd);
        }
        private static void InsereDadosPessoaisFicticios(CurriculumBdContext bd)
        {
            //GaranteExistenciaDadosPessoais(bd, "João", "rua", "912585214", "f@peo.pt");
            //GaranteExistenciaDadosPessoais(bd, "Maria");
            //GaranteExistenciaDadosPessoais(bd, "Manuel");
        }

        private static void GaranteExistenciaDadosPessoais(CurriculumBdContext bd, string nome)
        {
            DadosPessoais dadospessoais = bd.DadosPessoais.FirstOrDefault(c => c.Nome == nome);
            if (dadospessoais == null)
            {
                dadospessoais = new DadosPessoais { Nome = nome };
                bd.DadosPessoais.Add(dadospessoais);
                bd.SaveChanges();
            }
        }
        private static void InsereDadosFicticios(CurriculumBdContext bd)
        {
            
            //InsereComLingFicticiasParaTestarPaginacao(bd);
            //InsereEduForFicticiasParaTestarPaginacao(bd);
            //InsereExpProfissionalFicticiasParaTestarPaginacao(bd);


            if (bd.DadosPessoais.Any()) return;
            if (bd.ComLing.Any()) return;
            if (bd.EduFor.Any()) return;
            if (bd.ExpProfissional.Any()) return;

            DadosPessoais dadosJoao = bd.DadosPessoais.FirstOrDefault(c => c.Nome == "João");
            DadosPessoais dadosMaria = bd.DadosPessoais.FirstOrDefault(c => c.Nome == "Maria");

            //bd.DadosPessoais.AddRange(new DadosPessoais[] {
            //    new DadosPessoais
            //    {
            //        Nome = "João",
            //        Morada = "Travessa",
            //        Telefone = "912345678",
            //        Email = "fe@g.pt"
            //    },
            //    new DadosPessoais
            //    {
            //        Nome = "Manuel",
            //        Morada = "Lugar",
            //        Telefone = "912345678",
            //        Email = "gt@g.pt"
            //    },
            //    new DadosPessoais
            //    {
            //        Nome = "Maria",
            //        Morada = "Travessa",
            //        Telefone = "912345678",
            //        Email = "fe@g.pt"
            //    }
            //});

            //bd.SaveChanges();

            bd.ComLing.AddRange(new ComLing[] {
                new ComLing
                {
                    Lingua = "A1",
                    CompreensaoOral = "A1",
                    Leitura = "A1",
                    ProducaoOral = "A1",
                    InteracaoOral = "A1",
                    Escrita= "A1",
                    DadosPessoais = dadosJoao

                },
                  new ComLing
                {
                    Lingua = "A1",
                    CompreensaoOral = "A1",
                    Leitura = "A1",
                    ProducaoOral = "A1",
                    InteracaoOral = "A1",
                    Escrita= "A1",
                    DadosPessoais = dadosMaria

                },
                 new ComLing
                {
                    Lingua = "B1",
                    CompreensaoOral = "B1",
                    Leitura = "A1",
                    ProducaoOral = "A1",
                    InteracaoOral = "A1",
                    Escrita= "A1",
                    DadosPessoais = dadosJoao

                },
            });

            bd.SaveChanges();
            bd.EduFor.AddRange(new EduFor[] {
                new EduFor
                {
                    DataInicio = new DateTime(2019,01,12),
                    DataFim = new DateTime(2019,01,12),
                    NomeFormacao = "Licenciatura",
                    EntFormadora = "Fep",
                    DadosPessoais = dadosJoao
                },
                   new EduFor
                {
                    DataInicio = new DateTime(2019,01,12),
                    DataFim = new DateTime(2019,01,12),
                    NomeFormacao = "Licenciatura",
                    EntFormadora = "Fep",
                    DadosPessoais = dadosMaria
                },
                   new EduFor
                {
                    DataInicio = new DateTime(2019,01,12),
                    DataFim = new DateTime(2019,01,12),
                    NomeFormacao = "Licenciatura",
                    EntFormadora = "Fep",
                    DadosPessoais = dadosJoao
                },
            });

            bd.SaveChanges();
            bd.ExpProfissional.AddRange(new ExpProfissional[] {
                new ExpProfissional
                {
                    DataInicio=  new DateTime(2019,01,12),
                    DataFim =  new DateTime(2019,01,12),
                    NomeEmpresa = "ised",
                    Funcao = "tratador",
                    DadosPessoais = dadosJoao
                    
                },
                    new ExpProfissional
                {
                    DataInicio=  new DateTime(2019,01,12),
                    DataFim =  new DateTime(2019,01,12),
                    NomeEmpresa = "ised",
                    Funcao = "tratador",
                    DadosPessoais = dadosMaria

                },
                    new ExpProfissional
                {
                    DataInicio=  new DateTime(2019,01,12),
                    DataFim =  new DateTime(2019,01,12),
                    NomeEmpresa = "ised",
                    Funcao = "tratador",
                    DadosPessoais = dadosJoao

                },
            });

            bd.SaveChanges();
        }

        private static void InsereExpProfissionalFicticiasParaTestarPaginacao(CurriculumBdContext bd)
        {
            for (int i = 0; i < 100; i++)
            {
                bd.ExpProfissional.Add(new ExpProfissional
                {
                    DataInicio = new DateTime(2019, 01, 12),
                    DataFim = new DateTime(2019, 01, 12),
                    NomeEmpresa = "Aola " + i,
                    Funcao = "joajf " + i
                });
            }
            bd.SaveChanges();
        }

        private static void InsereEduForFicticiasParaTestarPaginacao(CurriculumBdContext bd)
        {
            for (int i = 0; i < 100; i++)
            {
                bd.EduFor.Add(new EduFor
                {
                    DataInicio = new DateTime(2019,01,12),
                    DataFim = new DateTime(2019, 01, 12),
                    NomeFormacao = "Aola " + i,
                    EntFormadora = "joajf " + i
                });
            }
            bd.SaveChanges();
        }

        private static void InsereComLingFicticiasParaTestarPaginacao(CurriculumBdContext bd)
        {
            for (int i = 0; i < 100; i++)
            {
                bd.ComLing.Add(new ComLing
                {
                    Lingua = "A " + i,
                    CompreensaoOral = "B" + i,
                    Leitura = "A " + i,
                    ProducaoOral = "B " + i,
                    InteracaoOral = "A " + i,
                    Escrita = "B " + i
                });
            }

            bd.SaveChanges();
        }

        private static void InsereDadosPessoaisFicticiosParaTestarPaginacao(CurriculumBdContext bd)
        {


            for (int i = 0; i < 20; i++)
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

