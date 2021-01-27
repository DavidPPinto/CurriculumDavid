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
    public class DadosPessoaisController : Controller
    {
        private readonly CurriculumBdContext _context;

        public DadosPessoaisController(CurriculumBdContext context)
        {
            _context = context;
        }

        // GET: DadosPessoais
        public async Task<IActionResult> Index()
        {
            return View(await _context.DadosPessoais.ToListAsync());
        }

        // GET: DadosPessoais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadosPessoais = await _context.DadosPessoais
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
        public async Task<IActionResult> Create([Bind("DadosPessoaisId,Nome,Morada,Telefone,Email")] DadosPessoais dadosPessoais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dadosPessoais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dadosPessoais);
        }

        // GET: DadosPessoais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadosPessoais = await _context.DadosPessoais.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("DadosPessoaisId,Nome,Morada,Telefone,Email")] DadosPessoais dadosPessoais)
        {
            if (id != dadosPessoais.DadosPessoaisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dadosPessoais);
                    await _context.SaveChangesAsync();
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

            var dadosPessoais = await _context.DadosPessoais
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
            var dadosPessoais = await _context.DadosPessoais.FindAsync(id);
            _context.DadosPessoais.Remove(dadosPessoais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DadosPessoaisExists(int id)
        {
            return _context.DadosPessoais.Any(e => e.DadosPessoaisId == id);
        }
    }
}
