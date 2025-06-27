using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MinhaPrimeiraApi.Controllers
{
    [ApiController] // Essa classe e um controlador de API
    [Route("api/[controller]")] // Define a rota base para este controlador
    public class ProdutosController : ControllerBase // Herda de ControllerBase para funcionalidades de API
    {
        private readonly ApiDbContext _context;

        public ProdutosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET /api/produtos
        // Endpoint para obter todos os produtos
        [HttpGet]// Indica que este método responde a requisições GET
        // Revisar: Programacao Assincrona
        public async Task<ActionResult<IEnumerable<Produto>>> Get() // Metodo agora e async task
        {
            return Ok(await _context.Produtos.ToListAsync());
        }

        // GET /api/produtos/{id}
        // Endpoint para obter um produto especifico por ID
        [HttpGet("{id}")] // Este metodo responde a GET e espera um {id} na rota
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                return NotFound($"Produto com ID {id} nao encontrado");
            }

            return Ok(produto);
        }

        // POST /api/produtos
        // Endpoint para adicionar um novo produto
        [HttpPost]
        public async Task<ActionResult<Produto>> Post([FromBody] Produto novoProduto)
        {
            if (await _context.Produtos.AnyAsync(p => p.Id == novoProduto.Id))
            {
                return BadRequest($"Ja existe um produto com o ID {novoProduto.Id}.");
            }

            _context.Produtos.Add(novoProduto);
            await _context.SaveChangesAsync(); // SALVA AS MUDANCAS NO BANCO DE DADOS

            return CreatedAtAction(nameof(GetById), new { id = novoProduto.Id }, novoProduto);
        }

        // PUT /api/produtos/{id}
        // Endpoint para atualizar um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produtoAtualizado)
        {
            if (id != produtoAtualizado.Id)
            {
                return BadRequest("O ID da rota nao corresponde ao ID do produto no corpo da requisicao");
            }

            var produtoExistente = await _context.Produtos.FindAsync(id);

            if (produtoExistente == null)
            {
                return NotFound($"Produto com ID {id} nao encontrado para atualizacao");
            }

            await _context.SaveChangesAsync(); // SALVA AS MUDANCAS NO BANCO DE DADOS

            return NoContent();
        }

        // DELETE /api/produtos/{id}
        // Endpoint para remover um produto
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produtoParaRemover = await _context.Produtos.FindAsync(id);

            if (produtoParaRemover == null)
            {
                return NotFound($"Produto com ID {id} não encontrado para remoção.");
            }

            _context.Produtos.Remove(produtoParaRemover); // Marca para remocao no DbSet
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}