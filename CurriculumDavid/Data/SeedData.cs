using CurriculumDavid.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculumDavid.Data
{
    public class SeedData
    {

        private const string NOME_UTILIZADOR_ADMIN_PADRAO = "admin@ipg.pt";
        private const string PASSWORD_UTILIZADOR_ADMIN_PADRAO = "Secret123$";
        private const string NOME_UTILIZADOR_FICTICIO = "joao@ipg.pt";


        private const string ROLE_ADIMINISTRADOR = "Administrador";
        private const string ROLE_UTILIZADOR = "Utilizador";
        private const string ROLE_GESTOR_DADOS = "GestorDados";

        internal static void PreencheDadosPessoais(CurriculumBdContext bd)
        {
            InsereDadosPessoaisFicticios(bd);
            InsereUtilizarFinaisFicticios(bd);
            
        }

        private static void InsereUtilizarFinaisFicticios(CurriculumBdContext bd)
        {
            if (!bd.Utilizador.Any(c => c.Email == NOME_UTILIZADOR_FICTICIO))
            {
                Utilizador c = new Utilizador
                {
                    Nome = "João",
                    Email = NOME_UTILIZADOR_FICTICIO,
                    Telefone = "930233223"
                };

                bd.Utilizador.Add(c);
                bd.SaveChanges();
            }
        }

        private static void InsereDadosPessoaisFicticios(CurriculumBdContext bd)
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
            });

            DadosPessoais dadosMaria = new DadosPessoais
            {
                Nome = "Maria",
                Morada = "Travessa",
                Telefone = "912345678",
                Email = "fe@g.pt"
            };

            bd.DadosPessoais.Add(dadosMaria);

            bd.SaveChanges();

            InsereComLingFicticiasParaTestarPaginacao(bd, dadosMaria);
            InsereEduForFicticiasParaTestarPaginacao(bd, dadosMaria);
            InsereExpProfissionalFicticiasParaTestarPaginacao(bd, dadosMaria);
        }



        private static void InsereExpProfissionalFicticiasParaTestarPaginacao(CurriculumBdContext bd, DadosPessoais pessoa)
        {

            if (bd.ExpProfissional.Any()) return;

            for (int i = 0; i < 100; i++)
            {
                bd.ExpProfissional.Add(new ExpProfissional
                {
                    DataInicio = new DateTime(2019, 01, 12),
                    DataFim = new DateTime(2019, 01, 12),
                    NomeEmpresa = "Aola " + i,
                    Funcao = "joajf " + i,
                    DadosPessoais = pessoa
                });
            }
            bd.SaveChanges();
        }

        private static void InsereEduForFicticiasParaTestarPaginacao(CurriculumBdContext bd, DadosPessoais pessoa)
        {

            if (bd.EduFor.Any()) return;
             
            for (int i = 0; i < 100; i++)
            {
                bd.EduFor.Add(new EduFor
                {
                    DataInicio = new DateTime(2019,01,12),
                    DataFim = new DateTime(2019, 01, 12),
                    NomeFormacao = "Aola " + i,
                    EntFormadora = "joajf " + i,
                    DadosPessoais = pessoa
                });
            }
            bd.SaveChanges();
        }

        private static void InsereComLingFicticiasParaTestarPaginacao(CurriculumBdContext bd, DadosPessoais pessoa)
        {
            if (bd.ComLing.Any()) return;

            for (int i = 0; i < 100; i++)
            {
                bd.ComLing.Add(new ComLing
                {
                    Lingua = "A " + i,
                    CompreensaoOral = "B" + i,
                    Leitura = "A " + i,
                    ProducaoOral = "B " + i,
                    InteracaoOral = "A " + i,
                    Escrita = "B " + i,
                    DadosPessoais = pessoa
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



        internal static async Task InsereUtilizadoresFicticiosAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser utilizador = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, NOME_UTILIZADOR_FICTICIO, "Secret123$");
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, utilizador, ROLE_UTILIZADOR);

            IdentityUser gestor = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, "maria@ipg.pt", "Secret123$");
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, gestor, ROLE_GESTOR_DADOS);
        }

        internal static async Task InsereRolesAsync(RoleManager<IdentityRole> gestorRoles)
        {
            await CriaRoleSeNecessario(gestorRoles, ROLE_ADIMINISTRADOR);
            await CriaRoleSeNecessario(gestorRoles, ROLE_UTILIZADOR);
            await CriaRoleSeNecessario(gestorRoles, ROLE_GESTOR_DADOS);
            //await CriaRoleSeNecessario(gestorRoles, "PodeAlterarPrecoProdutos");
        }

        private static async Task CriaRoleSeNecessario(RoleManager<IdentityRole> gestorRoles, string funcao)
        {
            if (!await gestorRoles.RoleExistsAsync(funcao))
            {
                IdentityRole role = new IdentityRole(funcao);
                await gestorRoles.CreateAsync(role);
            }
        }

        internal static async Task InsereAdministradorPadraoAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser utilizador = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, NOME_UTILIZADOR_ADMIN_PADRAO, PASSWORD_UTILIZADOR_ADMIN_PADRAO);
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, utilizador, ROLE_ADIMINISTRADOR);
        }
        private static async Task AdicionaUtilizadorRoleSeNecessario(UserManager<IdentityUser> gestorUtilizadores, IdentityUser utilizador, string role)
        {
            if (!await gestorUtilizadores.IsInRoleAsync(utilizador, role))
            {
                await gestorUtilizadores.AddToRoleAsync(utilizador, role);
            }
        }

        private static async Task<IdentityUser> CriaUtilizadorSeNaoExiste(UserManager<IdentityUser> gestorUtilizadores, string nomeUtilizador, string password)
        {
            IdentityUser utilizador = await gestorUtilizadores.FindByNameAsync(nomeUtilizador);

            if (utilizador == null)
            {
                utilizador = new IdentityUser(nomeUtilizador);
                await gestorUtilizadores.CreateAsync(utilizador, password);
            }

            return utilizador;
        }
    }


}

