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
    public class ComLingsController : Controller
    {
        private readonly CurriculumBdContext _context;

        public ComLingsController(CurriculumBdContext context)
        {
            _context = context;
        }

        // GET: ComLings
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComLing.ToListAsync());
        }

        // GET: ComLings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comLing = await _context.ComLing
                .FirstOrDefaultAsync(m => m.CompetenciasId == id);
            if (comLing == null)
            {
                return NotFound();
            }

            return View(comLing);
        }

        // GET: ComLings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComLings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetenciasId,Lingua,CompreensaoOral,Leitura,ProducaoOral,InteracaoOral,Escrita")] ComLing comLing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comLing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comLing);
        }

        // GET: ComLings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comLing = await _context.ComLing.FindAsync(id);
            if (comLing == null)
            {
                return NotFound();
            }
            return View(comLing);
        }

        // POST: ComLings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetenciasId,Lingua,CompreensaoOral,Leitura,ProducaoOral,InteracaoOral,Escrita")] ComLing comLing)
        {
            if (id != comLing.CompetenciasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comLing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComLingExists(comLing.CompetenciasId))
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
            return View(comLing);
        }

        // GET: ComLings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comLing = await _context.ComLing
                .FirstOrDefaultAsync(m => m.CompetenciasId == id);
            if (comLing == null)
            {
                return NotFound();
            }

            return View(comLing);
        }

        // POST: ComLings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comLing = await _context.ComLing.FindAsync(id);
            _context.ComLing.Remove(comLing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComLingExists(int id)
        {
            return _context.ComLing.Any(e => e.CompetenciasId == id);
        }
    }
}
