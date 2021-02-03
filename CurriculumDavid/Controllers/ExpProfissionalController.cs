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

namespace CurriculumDavid.Controllers
{
    [Authorize(Roles = "Administrador, GestorDados, Utilizador")]
    public class ExpProfissionalController : Controller
    {
        private readonly CurriculumBdContext bd;

        public ExpProfissionalController(CurriculumBdContext context)
        {
            bd = context;
        }

        // GET: ExpProfissional
        public async Task<IActionResult> Index(string nomePesquisar, int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await bd.ExpProfissional.Where(p => nomePesquisar == null || p.NomeEmpresa.Contains(nomePesquisar)).CountAsync(),
                PaginaAtual = pagina
            };

            List<ExpProfissional> expProfissionals = await bd.ExpProfissional.Where(p => nomePesquisar == null || p.NomeEmpresa.Contains(nomePesquisar))
                .Include(p => p.DadosPessoais)
               .OrderByDescending(p => p.DataFim)
               .Skip(paginacao.ItemsPorPagina * (pagina - 1))
               .Take(paginacao.ItemsPorPagina)
               .ToListAsync();

            ListaDadosViewModel modelo = new ListaDadosViewModel
            {
                Paginacao = paginacao,
                ExpProfissional = expProfissionals,
                NomePesquisar = nomePesquisar
            };

            return base.View(modelo);

        }

        // GET: ExpProfissional/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expProfissional = await bd.ExpProfissional
		        .Include(e => e.DadosPessoais)
                .FirstOrDefaultAsync(m => m.ExpProfissionalId == id);
            if (expProfissional == null)
            {
                return NotFound();
            }

            return View(expProfissional);
        }

        // GET: ExpProfissional/Create
        public IActionResult Create()
        {
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email");
            return View();
        }

        // POST: ExpProfissional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpProfissionalId,DataInicio,DataFim,NomeEmpresa,Funcao,DadosPessoaisId")] ExpProfissional expProfissional)
        {
            if (ModelState.IsValid)
            {
                bd.Add(expProfissional);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", expProfissional.DadosPessoaisId);
            return View(expProfissional);
        }

        // GET: ExpProfissional/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expProfissional = await bd.ExpProfissional.FindAsync(id);
            if (expProfissional == null)
            {
                return NotFound();
            }
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", expProfissional.DadosPessoaisId);
            return View(expProfissional);
        }

        // POST: ExpProfissional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpProfissionalId,DataInicio,DataFim,NomeEmpresa,Funcao,DadosPessoaisId")] ExpProfissional expProfissional)
        {
            if (id != expProfissional.ExpProfissionalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(expProfissional);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpProfissionalExists(expProfissional.ExpProfissionalId))
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
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", expProfissional.DadosPessoaisId);
            return View(expProfissional);
        }

        // GET: ExpProfissional/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expProfissional = await bd.ExpProfissional
 		.Include(e => e.DadosPessoais)
                .FirstOrDefaultAsync(m => m.ExpProfissionalId == id);
            if (expProfissional == null)
            {
                return NotFound();
            }

            return View(expProfissional);
        }

        // POST: ExpProfissional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expProfissional = await bd.ExpProfissional.FindAsync(id);
            bd.ExpProfissional.Remove(expProfissional);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpProfissionalExists(int id)
        {
            return bd.ExpProfissional.Any(e => e.ExpProfissionalId == id);
        }
    }
}
