using Ingenico.Barcode.Domain.Entites;

namespace Ingenico.Barcode.Domain.Repository {
    public interface IProdutoRepository {
    
        Task<ProdutoEntity> ObterProdutoAsync(int id);
        Task<List<ProdutoEntity>> ObterTodosProdutosAsync();
        Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto);
        Task<ProdutoEntity> AtualizarProdutoAsync(ProdutoEntity produto);
        Task<ProdutoEntity> ExcluirProdutoAsync(ProdutoEntity produto);

        //Task<List<CategoriaEntity>> ObterCategoriasAsync(int id);
        //Task<List<TagEntity>> ObterTagsAsync(int id);

        //Task<CategoriaEntity> ObterCategoriaPrincipalAsync(int id);
        //Task<TagEntity> ObterTagPrincipalAsync(int id);


    }
}
