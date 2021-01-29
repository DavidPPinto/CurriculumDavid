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
    public class EduForController : Controller
    {
        private readonly CurriculumBdContext bd;

        public EduForController(CurriculumBdContext context)
        {
            bd = context;
        }

        // GET: EduFor
        public async Task<IActionResult> Index(int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await bd.EduFor.CountAsync(),
                PaginaAtual = pagina
            };

            List<EduFor> eduFors = await bd.EduFor
                .OrderByDescending(p =>p.DataFim)
                .Skip(paginacao.ItemsPorPagina * (pagina - 1))
                .Take(paginacao.ItemsPorPagina)
                .ToListAsync();

            ListaDadosViewModel modelo = new ListaDadosViewModel
            {
                Paginacao = paginacao,
                EduFor = eduFors
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
            return View();
        }

        // POST: EduFor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduForId,DataInicio,DataFim,NomeFormacao,EntFormadora")] EduFor eduFor)
        {
            if (ModelState.IsValid)
            {
                bd.Add(eduFor);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(eduFor);
        }

        // POST: EduFor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduForId,DataInicio,DataFim,NomeFormacao,EntFormadora")] EduFor eduFor)
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
