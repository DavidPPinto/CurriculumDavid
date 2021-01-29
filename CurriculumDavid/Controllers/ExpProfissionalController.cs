using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CurriculumDavid.Data;
using CurriculumDavid.Models;

namespace CurriculumDavid.Controllers
{
    public class ExpProfissionalController : Controller
    {
        private readonly CurriculumBdContext bd;

        public ExpProfissionalController(CurriculumBdContext context)
        {
            bd = context;
        }

        // GET: ExpProfissional
        public async Task<IActionResult> Index(int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await bd.ExpProfissional.CountAsync(),
                PaginaAtual = pagina
            };

            List<ExpProfissional> expProfissionals = await bd.ExpProfissional
               .OrderByDescending(p => p.DataFim)
               .Skip(paginacao.ItemsPorPagina * (pagina - 1))
               .Take(paginacao.ItemsPorPagina)
               .ToListAsync();

            ListaDadosViewModel modelo = new ListaDadosViewModel
            {
                Paginacao = paginacao,
                ExpProfissional = expProfissionals
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
            return View();
        }

        // POST: ExpProfissional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpProfissionalId,DataInicio,DataFim,NomeEmpresa,Funcao")] ExpProfissional expProfissional)
        {
            if (ModelState.IsValid)
            {
                bd.Add(expProfissional);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(expProfissional);
        }

        // POST: ExpProfissional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpProfissionalId,DataInicio,DataFim,NomeEmpresa,Funcao")] ExpProfissional expProfissional)
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
