using Ingenico.Barcode.Domain.Entites;

namespace Ingenico.Barcode.Domain.Repository {
    public interface ITagRepository { 
        Task<TagEntity> ObterTagAsync(Guid id);
        Task<List<TagEntity>> ObterTodasTagsAsync();
        Task<TagEntity> CadastrarTagAsync(TagEntity tag);
        Task<TagEntity> AtualizarTagAsync(TagEntity tag);
        Task<TagEntity> ExcluirTagAsync(TagEntity tag);
        Task<TagEntity?> ObterTagPorNomeAsync(string nome);
    }
}
