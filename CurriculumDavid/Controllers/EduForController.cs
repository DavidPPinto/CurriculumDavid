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
        private readonly CurriculumBdContext _context;

        public EduForController(CurriculumBdContext context)
        {
            _context = context;
        }

        // GET: EduFor
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduFor.ToListAsync());
        }

        // GET: EduFor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduFor = await _context.EduFor
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
                _context.Add(eduFor);
                await _context.SaveChangesAsync();
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

            var eduFor = await _context.EduFor.FindAsync(id);
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
                    _context.Update(eduFor);
                    await _context.SaveChangesAsync();
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

            var eduFor = await _context.EduFor
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
            var eduFor = await _context.EduFor.FindAsync(id);
            _context.EduFor.Remove(eduFor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduForExists(int id)
        {
            return _context.EduFor.Any(e => e.EduForId == id);
        }
    }
}
