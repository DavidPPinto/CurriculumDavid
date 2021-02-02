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
    [Authorize]
    public class EduForController : Controller
    {
        private readonly CurriculumBdContext bd;

        public EduForController(CurriculumBdContext context)
        {
            bd = context;
        }

        // GET: EduFor
        public async Task<IActionResult> Index(string nomePesquisar, int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await bd.EduFor.Where(p => nomePesquisar == null || p.NomeFormacao.Contains(nomePesquisar)).CountAsync(),
                PaginaAtual = pagina
            };

            List<EduFor> eduFors = await bd.EduFor.Where(p => nomePesquisar == null || p.NomeFormacao.Contains(nomePesquisar))
                .Include(p => p.DadosPessoais)
                .OrderByDescending(p =>p.DataFim)
                .Skip(paginacao.ItemsPorPagina * (pagina - 1))
                .Take(paginacao.ItemsPorPagina)
                .ToListAsync();

            ListaDadosViewModel modelo = new ListaDadosViewModel
            {
                Paginacao = paginacao,
                EduFor = eduFors,
                NomePesquisar = nomePesquisar
            };

            return base.View(modelo);
        }
        // GET: EduFor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduFor = await bd.EduFor
                .Include(e => e.DadosPessoais)
                .FirstOrDefaultAsync(m => m.EduForId == id);
            if (eduFor == null)
            {
                return NotFound();
            }

            return View(eduFor);
        }

        // GET: EduFor/Create
        public IActionResult Create()
        {
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email");
            return View();
        }

        // POST: EduFor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduForId,DataInicio,DataFim,NomeFormacao,EntFormadora,DadosPessoaisId")] EduFor eduFor)
        {
            if (ModelState.IsValid)
            {
                bd.Add(eduFor);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", eduFor.DadosPessoaisId);
            return View(eduFor);
        }

        // GET: EduFor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduFor = await bd.EduFor.FindAsync(id);
            if (eduFor == null)
            {
                return NotFound();
            }
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", eduFor.DadosPessoaisId);
            return View(eduFor);
        }

        // POST: EduFor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduForId,DataInicio,DataFim,NomeFormacao,EntFormadora,DadosPessoaisId")] EduFor eduFor)
        {
            if (id != eduFor.EduForId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(eduFor);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduForExists(eduFor.EduForId))
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
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", eduFor.DadosPessoaisId);
            return View(eduFor);
        }

        // GET: EduFor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduFor = await bd.EduFor
		.Include(e => e.DadosPessoais)
                .FirstOrDefaultAsync(m => m.EduForId == id);
            if (eduFor == null)
            {
                return NotFound();
            }

            return View(eduFor);
        }

        // POST: EduFor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduFor = await bd.EduFor.FindAsync(id);
            bd.EduFor.Remove(eduFor);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduForExists(int id)
        {
            return bd.EduFor.Any(e => e.EduForId == id);
        }
    }
}
