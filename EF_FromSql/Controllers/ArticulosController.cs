using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF_FromSql.Models;

namespace EF_FromSql.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly NetCorePart2TechSessionContext _context;

        public ArticulosController(NetCorePart2TechSessionContext context)
        {
            _context = context;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articulos.ToListAsync());
        }

        //Get: Articulos/FromSql
        public async Task<IActionResult> FromSql()
        {
            var contenido = await _context.Articulos.FromSql("SELECT * FROM Articulos WHERE Precio=1").Where(c => c.Name == "Fresas").ToListAsync();
            return View(contenido);
        }

        // GET: Articulos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulos == null)
            {
                return NotFound();
            }

            return View(articulos);
        }

        // GET: Articulos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Precio")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articulos);
        }

        // GET: Articulos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return NotFound();
            }
            return View(articulos);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Precio")] Articulos articulos)
        {
            if (id != articulos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticulosExists(articulos.Id))
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
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulos == null)
            {
                return NotFound();
            }

            return View(articulos);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var articulos = await _context.Articulos.FindAsync(id);
            _context.Articulos.Remove(articulos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticulosExists(long id)
        {
            return _context.Articulos.Any(e => e.Id == id);
        }
    }
}
