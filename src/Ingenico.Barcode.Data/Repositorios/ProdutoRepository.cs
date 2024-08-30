
using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ingenico.Barcode.Data.Repositorios {
    public class ProdutoRepository : IProdutoRepository {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ProdutoEntity> AtualizarProdutoAsync(ProdutoEntity produto) {
            _context.Produto.Update(produto);

            return produto;
        }

        public async Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto) {
            _context.Produto.Add(produto);

            return produto;
        }

        public async Task<ProdutoEntity> ExcluirProdutoAsync(ProdutoEntity produto) {
            _context.Produto.Remove(produto);

            return produto;
        }

        public async Task<ProdutoEntity> ObterProdutoAsync(Guid id) {

            var produto = await _context.Produto
                .Include(p => p.ProdutoCategoria)
                .Include(p => p.ProdutoTag)
                .FirstOrDefaultAsync(p => p.ProdutoId == id);
            return produto;
        }

        public async Task<List<ProdutoEntity>> ObterTodosProdutosAsync() {
            var produtos = await _context.Produto
                .ToListAsync();
            return produtos;
        }
        public IQueryable<ProdutoEntity> ObterQueryable()
        {
            return _context.Produto.AsQueryable();
        }

        // Novo método para remover associações de categorias
        public void RemoverCategorias(ProdutoEntity produto) {
            var categoriasParaRemover = produto.ProdutoCategoria.ToList();
            _context.Set<ProdutoCategoria>().RemoveRange(categoriasParaRemover);
        }

        // Novo método para remover associações de tags
        public void RemoverTags(ProdutoEntity produto) {
            var tagsParaRemover = produto.ProdutoTag.ToList();
            _context.Set<ProdutoTag>().RemoveRange(tagsParaRemover);
        }
    }
}
