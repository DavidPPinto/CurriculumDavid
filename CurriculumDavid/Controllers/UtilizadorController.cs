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
using Microsoft.AspNetCore.Identity;

namespace CurriculumDavid.Controllers
{
    public class UtilizadorController : Controller
    {
        private readonly CurriculumBdContext bd;
        private readonly UserManager<IdentityUser> _gestorUtilizadores;


        public UtilizadorController(CurriculumBdContext context, UserManager<IdentityUser> gestorUtilizadores)
        {
            bd = context;
            _gestorUtilizadores = gestorUtilizadores;
        }

        // GET: Utilizador
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await bd.Utilizador.ToListAsync());
        }

        // GET: Utilizador/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            Utilizador utilizador;

            if (id != null)
            {
                utilizador = await bd.Utilizador.SingleOrDefaultAsync(u => u.UtilizadorId == id);
                if(utilizador == null)
                {
                    return NotFound();
                }
            }
            else 
            {
                if (!User.IsInRole("Utilizador")) 
                {
                    return NotFound();
                }
            }

             utilizador = await bd.Utilizador.SingleOrDefaultAsync(c => c.Email == User.Identity.Name);
            
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // GET: Utilizador/Create
        public IActionResult Registo()
        {
            return View();
        }

        // POST: Utilizador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registo(RegistoUtilizadoreViewModel infoUtilizador)
        {
            IdentityUser utilizadorF = await _gestorUtilizadores.FindByNameAsync(infoUtilizador.Email);

            if (utilizadorF != null)
            {
                ModelState.AddModelError("Email", "Já existe um cliente/utilizador com o email que especificou.");
            }

            utilizadorF = new IdentityUser(infoUtilizador.Email);
            IdentityResult resultado = await _gestorUtilizadores.CreateAsync(utilizadorF, infoUtilizador.Password);
            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível fazer o registo. Por favor tente mais tarde novamente e se o problema persistir contacte a assistência.");
            }
            else
            {
                await _gestorUtilizadores.AddToRoleAsync(utilizadorF, "Utilizador");
            }

            if (!ModelState.IsValid)
            {
                return View(infoUtilizador);
            }


            Utilizador utilizador = new Utilizador
            {
                Nome = infoUtilizador.Nome,
                Email = infoUtilizador.Email,
                Telefone = infoUtilizador.Telefone
            };

            bd.Add(utilizador);
            await bd.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        // GET: Utilizador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await bd.Utilizador.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilizadorId,Nome,Telefone,Email")] Utilizador utilizador)
        {
            if (id != utilizador.UtilizadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(utilizador);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.UtilizadorId))
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
            return View(utilizador);
        }

        // GET: Utilizador/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await bd.Utilizador
                .FirstOrDefaultAsync(m => m.UtilizadorId == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // POST: Utilizador/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizador = await bd.Utilizador.FindAsync(id);
            bd.Utilizador.Remove(utilizador);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(int id)
        {
            return bd.Utilizador.Any(e => e.UtilizadorId == id);
        }
    }
}
