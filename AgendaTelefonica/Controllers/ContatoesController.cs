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
    public class ContatoesController : Controller
    {
        private readonly Contexto _contexto;
        private readonly ClassAuxiliar _class;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContatoesController(Contexto contexto, IWebHostEnvironment webHostEnvironment, ClassAuxiliar classAuxiliar)
        {
            _contexto = contexto;
            _class = classAuxiliar;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Contato> contato = await _contexto.Contatos.ToListAsync();
            return View(contato);
        }

        [HttpGet]
        // GET: Contatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contato = await _contexto.Contatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contato == null)
            {
                return NotFound();
            }

            return View(contato);
        }

        [HttpGet]
        // GET: Contatoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Idade,Telefone,TipoTelefone,Foto")] Contato contato, IFormFile foto)
        {
            if (foto != null)
            {
                string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                {
                    await foto.CopyToAsync(fileStream);
                    contato.Foto = "~/img/" + nomeFoto;
                }
            }
            else
            {
                contato.Foto = "~/img/usuario.jpg";
            }

            _contexto.Add(contato);
            await _contexto.SaveChangesAsync();
            TempData["ContatoNovo"] = $"Contato com nome {contato.Nome} {contato.Sobrenome} incluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Contatoes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Contato contato = await _contexto.Contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            TempData["Foto"] = contato.Foto;
            return View(contato);

        }

        // POST: Contatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Idade,Telefone,TipoTelefone,Foto")] Contato contato, IFormFile foto)
        {
            if (id != contato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(contato);
                    await _contexto.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExists(contato.Id))
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
            return View(contato);
        }

        [HttpDelete]
        // GET: Contatoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var contato = await _contexto.Contatos.FindAsync(id);
            _contexto.Contatos.Remove(contato);
            await _contexto.SaveChangesAsync();
            TempData["ContatoExcluido"] = $"Contato com nome {contato.Nome} {contato.Sobrenome} excluído com sucesso!";
            _class.id = 0;
            return RedirectToAction(nameof(Index));
        }


        private bool ContatoExists(int id)
        {
            return _contexto.Contatos.Any(e => e.Id == id);
        }

        
        public async Task<IActionResult> TesteExclusao(int id)
        {
            Delete(_class.id);
            return View();
        }
    }
}
