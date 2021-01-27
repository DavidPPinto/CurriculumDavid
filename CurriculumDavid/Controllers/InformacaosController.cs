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
    public class InformacaosController : Controller
    {
        private readonly BdContext _context;

        public InformacaosController(BdContext context)
        {
            _context = context;
        }

        // GET: Informacaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Informacao.ToListAsync());
        }

        // GET: Informacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacao = await _context.Informacao
                .FirstOrDefaultAsync(m => m.InformacaoId == id);
            if (informacao == null)
            {
                return NotFound();
            }

            return View(informacao);
        }

        // GET: Informacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Informacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataFormacao")] Informacao informacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informacao);
        }

        // GET: Informacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacao = await _context.Informacao.FindAsync(id);
            if (informacao == null)
            {
                return NotFound();
            }
            return View(informacao);
        }

        // POST: Informacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataFormacao")] Informacao informacao)
        {
            if (id != informacao.InformacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformacaoExists(informacao.InformacaoId))
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
            return View(informacao);
        }

        // GET: Informacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacao = await _context.Informacao
                .FirstOrDefaultAsync(m => m.InformacaoId == id);
            if (informacao == null)
            {
                return NotFound();
            }

            return View(informacao);
        }

        // POST: Informacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informacao = await _context.Informacao.FindAsync(id);
            _context.Informacao.Remove(informacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformacaoExists(int id)
        {
            return _context.Informacao.Any(e => e.InformacaoId == id);
        }
    }
}
