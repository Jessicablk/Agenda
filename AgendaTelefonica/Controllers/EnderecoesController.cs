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
    public class EnderecoesController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EnderecoesController(Contexto contexto, IWebHostEnvironment webHostEnvironment)
        {
            _contexto = contexto;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var contexto = _contexto.Enderecos.Include(e => e.Contato);
                var returno = contexto.ToList();
                return View(await contexto.ToListAsync());
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

        // GET: Enderecoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _contexto.Enderecos
                .Include(e => e.Contato)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecoes/Create
        public IActionResult Create()
        {
            ViewData["IdContato"] = new SelectList(_contexto.Contatos, "Id", "Nome");
            return View();
        }

        // POST: Enderecoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rua,Bairro,Cidade,Estado,Cep,Numero,TipoEndereco,IdContato")] Endereco endereco)
        {
            _contexto.Add(endereco);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Enderecoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _contexto.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }
            ViewData["IdContato"] = new SelectList(_contexto.Contatos, "Id", "Nome", endereco.IdContato);
            return View(endereco);
        }

        // POST: Enderecoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rua,Bairro,Cidade,Estado,Cep,Numero,TipoEndereco,IdContato")] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(endereco);
                    await _contexto.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.Id))
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
            ViewData["IdContato"] = new SelectList(_contexto.Contatos, "Id", "Nome", endereco.IdContato);
            return View(endereco);
        }

        // GET: Enderecoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _contexto.Enderecos
                .Include(e => e.Contato)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _contexto.Enderecos.FindAsync(id);
            _contexto.Enderecos.Remove(endereco);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
            return _contexto.Enderecos.Any(e => e.Id == id);
        }
    }
}
