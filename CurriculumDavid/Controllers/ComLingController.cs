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
    public class ComLingController : Controller
    {
        private readonly CurriculumBdContext bd;

        public ComLingController(CurriculumBdContext context)
        {
            bd = context;
        }

        // GET: ComLing
        public async Task<IActionResult> Index(string nomePesquisar, int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await bd.ComLing.Where(p => nomePesquisar == null || p.Lingua.Contains(nomePesquisar)).CountAsync(),
                PaginaAtual = pagina
            };

            List<ComLing> comLing = await bd.ComLing.Where(p => nomePesquisar == null || p.Lingua.Contains(nomePesquisar))
                .Include(p=> p.DadosPessoais)
                .OrderBy(p => p.Lingua)
                .Skip(paginacao.ItemsPorPagina * (pagina - 1))
                .Take(paginacao.ItemsPorPagina)
                .ToListAsync();

            ListaDadosViewModel modelo = new ListaDadosViewModel
            {
                Paginacao = paginacao,
                ComLing = comLing,
                NomePesquisar = nomePesquisar
            };

            return base.View(modelo);
        }
      

        // GET: ComLing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comLing = await bd.ComLing
                .FirstOrDefaultAsync(m => m.CompetenciasId == id);
            if (comLing == null)
            {
                return NotFound();
            }

            return View(comLing);
        }

        // GET: ComLing/Create
        public IActionResult Create()
        {
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email");
            return View();
        }

        // POST: ComLing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetenciasId,Lingua,CompreensaoOral,Leitura,ProducaoOral,InteracaoOral,Escrita,DadosPessoaisId")] ComLing comLing)
        {
            if (ModelState.IsValid)
            {
                bd.Add(comLing);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", comLing.DadosPessoaisId);
            return View(comLing);
        }

        // GET: ComLing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comLing = await bd.ComLing.FindAsync(id);
            if (comLing == null)
            {
                return NotFound();
            }
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", comLing.DadosPessoaisId);
            return View(comLing);
        }

        // POST: ComLing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetenciasId,Lingua,CompreensaoOral,Leitura,ProducaoOral,InteracaoOral,Escrita,DadosPessoaisId")] ComLing comLing)
        {
            if (id != comLing.CompetenciasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(comLing);
                    await bd.SaveChangesAsync();
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
            ViewData["DadosPessoaisId"] = new SelectList(bd.DadosPessoais, "DadosPessoaisId", "Email", comLing.DadosPessoaisId);
            return View(comLing);
        }

        // GET: ComLing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comLing = await bd.ComLing
                .FirstOrDefaultAsync(m => m.CompetenciasId == id);
            if (comLing == null)
            {
                return NotFound();
            }

            return View(comLing);
        }

        // POST: ComLing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comLing = await bd.ComLing.FindAsync(id);
            bd.ComLing.Remove(comLing);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComLingExists(int id)
        {
            return bd.ComLing.Any(e => e.CompetenciasId == id);
        }
    }
}
