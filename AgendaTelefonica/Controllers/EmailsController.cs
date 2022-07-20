using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaTelefonica.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AgendaTelefonica.Controllers
{
    public class EmailsController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailsController(Contexto contexto, IWebHostEnvironment webHostEnvironment)
        {
            _contexto = contexto;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Email> email = await _contexto.Emails.ToListAsync();
            return View(email);
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _contexto.Emails
                .Include(e => e.Contato)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            ViewData["IdContato"] = new SelectList(_contexto.Contatos, "Id", "Nome");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,EnderecoEmail,IdContato")] Email email)
        {
            _contexto.Add(email);
            await _contexto.SaveChangesAsync();
            TempData["EmailNovo"] = $"E-mail com nome {email.Nome} incluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _contexto.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            ViewData["IdContato"] = new SelectList(_contexto.Contatos, "Id", "Nome", email.IdContato);
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EnderecoEmail,IdContato")] Email email)
        {
            if (id != email.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(email);
                    await _contexto.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.Id))
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
            ViewData["IdContato"] = new SelectList(_contexto.Contatos, "Id", "Nome", email.IdContato);
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _contexto.Emails
                .Include(e => e.Contato)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var email = await _contexto.Emails.FindAsync(id);
            _contexto.Emails.Remove(email);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(int id)
        {
            return _contexto.Emails.Any(e => e.Id == id);
        }
    }
}
