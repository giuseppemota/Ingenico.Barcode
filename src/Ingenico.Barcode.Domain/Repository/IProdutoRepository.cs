using Ingenico.Barcode.Domain.Entites;

namespace Ingenico.Barcode.Domain.Repository {
    public interface IProdutoRepository {
    
        Task<ProdutoEntity> ObterProdutoAsync(Guid id);
        Task<List<ProdutoEntity>> ObterTodosProdutosAsync();
        Task<List<ProdutoEntity>> ObterProdutosPorTagsAsync(List<ProdutoTag> produtoTags);
        Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto);
        Task<ProdutoEntity> AtualizarProdutoAsync(ProdutoEntity produto);
        Task<ProdutoEntity> ExcluirProdutoAsync(ProdutoEntity produto);
        IQueryable<ProdutoEntity> ObterQueryable();

        void RemoverCategorias(ProdutoEntity produto);
        void RemoverTags(ProdutoEntity produto);



        //Task<List<CategoriaEntity>> ObterCategoriasAsync(int id);
        //Task<List<TagEntity>> ObterTagsAsync(int id);

        //Task<CategoriaEntity> ObterCategoriaPrincipalAsync(int id);
        //Task<TagEntity> ObterTagPrincipalAsync(int id);


    }
}
