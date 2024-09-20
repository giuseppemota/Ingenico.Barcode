
using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ingenico.Barcode.Data.Repositorios
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoEntity> AtualizarProdutoAsync(ProdutoEntity produto)
        {
            _context.Produto.Update(produto);

            return produto;
        }

        public async Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto)
        {
            _context.Produto.Add(produto);

            return produto;
        }

        public async Task<ProdutoEntity> ExcluirProdutoAsync(ProdutoEntity produto)
        {
            _context.Produto.Remove(produto);

            return produto;
        }

        public async Task<ProdutoEntity> ObterProdutoAsync(Guid id)
        {

            var produto = await _context.Produto
                .Include(p => p.ProdutoCategoria)
                .Include(p => p.ProdutoTag)
                .FirstOrDefaultAsync(p => p.ProdutoId == id);
            return produto;
        }

        public async Task<List<ProdutoEntity>> ObterProdutosPorTagsAsync(List<ProdutoTag> produtoTags)
        {
            // Pegue os IDs das tags associadas ao produto
            var tagIds = produtoTags.Select(pt => pt.TagId).ToList();

            // Verificar se tagIds e produtoTags não estão vazios
            if (!tagIds.Any())
            {
                throw new ArgumentException("Nenhuma tag associada ao produto.");
            }

            // Consulte os produtos que possuem essas tags e exclua o produto original
            var produtosSimilares = await _context.ProdutoTag
                .Where(pt => tagIds.Contains(pt.TagId) && pt.ProdutoId != produtoTags.First().ProdutoId) // Exclui o produto original
                .Include(pt => pt.Produto) // Inclui a entidade Produto
                    .ThenInclude(p => p.ProdutoCategoria) // Inclui as categorias relacionadas
                .Include(pt => pt.Produto) // Inclui novamente para pegar a entidade Produto
                    .ThenInclude(p => p.ProdutoTag) // Inclui as tags relacionadas
                .Select(pt => pt.Produto) // Seleciona o produto relacionado a cada tag
                .Distinct() // Remove duplicatas
                .ToListAsync(); // Executa a consulta

            // Verificar se encontrou produtos similares
            if (!produtosSimilares.Any())
            {
                Console.WriteLine("Nenhum produto similar encontrado.");
                return new List<ProdutoEntity>();
            }

            // Agora vamos contar quantas tags cada produto compartilha
            var produtosComTagsEmComum = produtosSimilares
                .Select(p => new
                {
                    Produto = p,
                    TagsEmComum = p.ProdutoTag.Count(pt => tagIds.Contains(pt.TagId)) // Contar quantas tags em comum
                })
                .Where(x => x.TagsEmComum >= 2) // Apenas produtos com pelo menos 2 tags em comum
                .OrderByDescending(x => x.TagsEmComum) // Ordena por mais tags em comum
                .Select(x => x.Produto)
                .ToList();

            return produtosComTagsEmComum;
        }

        public async Task<List<ProdutoEntity>> ObterTodosProdutosAsync()
        {
            var produtos = await _context.Produto
                .ToListAsync();
            return produtos;
        }
        public IQueryable<ProdutoEntity> ObterQueryable()
        {
            return _context.Produto.AsQueryable();
        }

        // Novo método para remover associações de categorias
        public void RemoverCategorias(ProdutoEntity produto)
        {
            var categoriasParaRemover = produto.ProdutoCategoria.ToList();
            _context.Set<ProdutoCategoria>().RemoveRange(categoriasParaRemover);
        }

        // Novo método para remover associações de tags
        public void RemoverTags(ProdutoEntity produto)
        {
            var tagsParaRemover = produto.ProdutoTag.ToList();
            _context.Set<ProdutoTag>().RemoveRange(tagsParaRemover);
        }
    }
}
