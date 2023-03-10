using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intergado.Models;

namespace Intergado.Controllers
{
    public class FazendasController : Controller
    {
        private readonly Contexto _context;

        public FazendasController(Contexto context)
        {
            _context = context;
        }

        // GET: Fazendas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Fazendas.ToListAsync());
        }

        // GET: Fazendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fazendas == null)
            {
                return NotFound();
            }

            var fazenda = await _context.Fazendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fazenda == null)
            {
                return NotFound();
            }

            return View(fazenda);
        }

        // GET: Fazendas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fazendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Fazenda fazenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fazenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fazenda);
        }

        // GET: Fazendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fazendas == null)
            {
                return NotFound();
            }

            var fazenda = await _context.Fazendas.FindAsync(id);
            if (fazenda == null)
            {
                return NotFound();
            }
            return View(fazenda);
        }

        // POST: Fazendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Fazenda fazenda)
        {
            if (id != fazenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fazenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FazendaExists(fazenda.Id))
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
            return View(fazenda);
        }

        // GET: Fazendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fazendas == null)
            {
                return NotFound();
            }

            var fazenda = await _context.Fazendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fazenda == null)
            {
                return NotFound();
            }

            return View(fazenda);
        }

        // POST: Fazendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fazendas == null)
            {
                return Problem("Entity set 'Contexto.Fazendas'  is null.");
            }
            var fazenda = await _context.Fazendas.FindAsync(id);
            if (fazenda != null)
            {
                _context.Fazendas.Remove(fazenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FazendaExists(int id)
        {
          return _context.Fazendas.Any(e => e.Id == id);
        }
    }
}
