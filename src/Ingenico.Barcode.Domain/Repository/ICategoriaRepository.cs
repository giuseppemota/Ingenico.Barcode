using Ingenico.Barcode.Domain.Entites;

namespace Ingenico.Barcode.Domain.Repository {
    public interface ICategoriaRepository {

        Task<CategoriaEntity> ObterCategoriaAsync(Guid id);
        Task<List<CategoriaEntity>> ObterTodasCategoriasAsync();
        Task<CategoriaEntity> CadastrarCategoriaAsync(CategoriaEntity categoria);
        Task<CategoriaEntity> AtualizarCategoriaAsync(CategoriaEntity categoria);
        Task<CategoriaEntity> ExcluirCategoriaAsync(CategoriaEntity categoria);
    }
}
