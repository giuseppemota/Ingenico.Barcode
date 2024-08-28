using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Microsoft.EntityFrameworkCore;


namespace Ingenico.Barcode.Data.Repositorios {
    public class TagRepository : ITagRepository {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<TagEntity> AtualizarTagAsync(TagEntity tag) {
            _context.Tag.Update(tag);

            return tag;
        }

        public async Task<TagEntity> CadastrarTagAsync(TagEntity tag) {
            _context.Tag.Add(tag);

            return tag;
        }

        public async Task<TagEntity> ExcluirTagAsync(TagEntity tag) {
            _context.Tag.Remove(tag);

            return tag;
        }

        public async Task<TagEntity> ObterTagAsync(Guid id) {
            var tag = await _context.Tag
                .FirstOrDefaultAsync(p => p.TagId == id);
            return tag;
        }


        public async Task<List<TagEntity>> ObterTodasTagsAsync() {
            var tags = await _context.Tag
                .ToListAsync();
            return tags;
        }
    }
}
