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
    public class AnimalsController : Controller
    {
        private readonly Contexto _context;

        public AnimalsController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListAnimals()
        {
            var contexto = _context.Animais.Include(a => a.Fazenda);
            return View(await contexto.ToListAsync());
        }

        public IActionResult Index(int fazendaId)
        {
            ViewBag.FazendaId = fazendaId;
            var animals = _context.Animais
                .Where(a => a.FazendaId == fazendaId)
                .Select(a => new Animal
                {
                    Id = a.Id,
                    Tag = a.Tag,
                })
                .ToList();

            return View(animals);
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais
                .Include(a => a.Fazenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }


        // GET: Animals/Create
       public IActionResult Create(int? fazendaId)
{
    var fazendas = _context.Fazendas.ToList();
    ViewBag.FazendaId = new SelectList(fazendas, "Id", "Nome", fazendaId);
    return View();
}

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tag,FazendaId")] Animal animal)
        {

            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
            var routeValues = new { fazendaId = animal.FazendaId };
            return base.RedirectToAction("Index", "Animals", routeValues);
            }

                return RedirectToAction(nameof(Index));
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["FazendaId"] = new SelectList(_context.Fazendas, "Id", "Nome", animal.FazendaId);
            return View(animal);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tag,FazendaId")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var routeValues = new { fazendaId = animal.FazendaId };
                return base.RedirectToAction("Index", "Animals", routeValues);
            }
            ViewData["FazendaId"] = new SelectList(_context.Fazendas, "Id", "Nome", animal.FazendaId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais
                .Include(a => a.Fazenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animais == null)
            {
                return Problem("Entity set 'Contexto.Animais'  is null.");
            }
            var animal = await _context.Animais.FindAsync(id);
            if (animal != null)
            {
                _context.Animais.Remove(animal);
            }
            
            await _context.SaveChangesAsync();
            var routeValues = new { fazendaId = animal.FazendaId };
            return base.RedirectToAction("Index", "Animals", routeValues);
        }

        private bool AnimalExists(int id)
        {
          return _context.Animais.Any(e => e.Id == id);
        }
    }
}
