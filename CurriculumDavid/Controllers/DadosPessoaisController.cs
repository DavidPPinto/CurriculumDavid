using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CurriculumDavid.Data;
using CurriculumDavid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CurriculumDavid.Controllers
{
    [Authorize(Roles = "Administrador, GestorDados, Utilizador")]
    public class DadosPessoaisController : Controller
    {
        private readonly CurriculumBdContext bd;

        public DadosPessoaisController(CurriculumBdContext context)
        {
            bd = context;
        }

        // GET: DadosPessoais
        public async Task<IActionResult> Index(int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await bd.DadosPessoais.CountAsync(),
                PaginaAtual = pagina
            };

            List<DadosPessoais> dadosPessoais = await bd.DadosPessoais
                .OrderBy(p => p.Nome)
                .Skip(paginacao.ItemsPorPagina * (pagina - 1))
                .Take(paginacao.ItemsPorPagina)
                .ToListAsync();

            ListaDadosViewModel modelo = new ListaDadosViewModel
            {
                Paginacao = paginacao,
                DadosPessoais = dadosPessoais
            };

            return base.View(modelo);
        }
      

        // GET: DadosPessoais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadosPessoais = await bd.DadosPessoais
                .FirstOrDefaultAsync(m => m.DadosPessoaisId == id);
            if (dadosPessoais == null)
            {
                return NotFound();
            }

            return View(dadosPessoais);
        }

        // GET: DadosPessoais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DadosPessoais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DadosPessoaisId,Nome,Morada,Telefone,Email")] DadosPessoais dadosPessoais, IFormFile ficheiroFoto)
        {
            if (ModelState.IsValid)
            {

                ActulizaFotoDadosPessoais(dadosPessoais, ficheiroFoto);

                bd.Add(dadosPessoais);
                await bd.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            ViewBag.Mensagem = "Dados adicionados com Sucesso";
            return View("Sucesso");

        }
        private static void ActulizaFotoDadosPessoais(DadosPessoais dadosPessoais, IFormFile ficheiroFoto)
            {
                if (ficheiroFoto != null && ficheiroFoto.Length > 0)
                {
                    using (var ficheiroMemoria = new MemoryStream())
                    {
                        ficheiroFoto.CopyTo(ficheiroMemoria);
                        dadosPessoais.Foto = ficheiroMemoria.ToArray();
                    }
                }
            }
          

        // GET: DadosPessoais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadosPessoais = await bd.DadosPessoais.FindAsync(id);
            if (dadosPessoais == null)
            {
                return NotFound();
            }
            return View(dadosPessoais);
        }

        // POST: DadosPessoais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DadosPessoaisId,Nome,Morada,Telefone,Email,DadosPessoaisId.Foto")] DadosPessoais dadosPessoais, IFormFile ficheiroFoto)
        {
            if (id != dadosPessoais.DadosPessoaisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ActulizaFotoDadosPessoais(dadosPessoais, ficheiroFoto);
                try
                {
                    bd.Update(dadosPessoais);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DadosPessoaisExists(dadosPessoais.DadosPessoaisId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dadosPessoais);
        }

        // GET: DadosPessoais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadosPessoais = await bd.DadosPessoais
                .FirstOrDefaultAsync(m => m.DadosPessoaisId == id);
            if (dadosPessoais == null)
            {
                return NotFound();
            }

            return View(dadosPessoais);
        }

        // POST: DadosPessoais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dadosPessoais = await bd.DadosPessoais.FindAsync(id);

            try
            {
                bd.DadosPessoais.Remove(dadosPessoais);
                await bd.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (bd.ComLing.Any(p => p.DadosPessoaisId == dadosPessoais.DadosPessoaisId))
                {
                    ViewBag.Mensagem = "Esta entrada não pode ser apagada porque já tem dados associados.";
                }
                if (bd.EduFor.Any(p => p.DadosPessoaisId == dadosPessoais.DadosPessoaisId))
                {
                    ViewBag.Mensagem = "Esta entrada não pode ser apagada porque já tem dados associados.";
                }
                if (bd.EduFor.Any(p => p.DadosPessoaisId == dadosPessoais.DadosPessoaisId))
                {
                    ViewBag.Mensagem = "Esta entrada não pode ser apagada porque já tem dados associados.";
                }
                else

                {
                    ViewBag.Mensagem = "Não foi possível eliminar a entrada. Tente novamente mais tarde e se o problema persistir contacte a assistência";
                }
                return View("Erro");
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool DadosPessoaisExists(int id)
        {
            return bd.DadosPessoais.Any(e => e.DadosPessoaisId == id);
        }
    }
}
