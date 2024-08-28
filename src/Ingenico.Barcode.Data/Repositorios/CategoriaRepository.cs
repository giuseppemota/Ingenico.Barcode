using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ingenico.Barcode.Data.Repositorios {
    public class CategoriaRepository : ICategoriaRepository {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<CategoriaEntity> AtualizarCategoriaAsync(CategoriaEntity categoria) {
            _context.Categoria.Update(categoria);

            return categoria;
        }

        public async Task<CategoriaEntity> CadastrarCategoriaAsync(CategoriaEntity categoria) {
            _context.Categoria.Add(categoria);

            return categoria;
        }

        public async Task<CategoriaEntity> ExcluirCategoriaAsync(CategoriaEntity categoria) {
            _context.Categoria.Remove(categoria);

            return categoria;
        }

        public async Task<CategoriaEntity> ObterCategoriaAsync(int id) {
            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(p => p.CategoriaId == id);
            return categoria;
        }


        public async Task<List<CategoriaEntity>> ObterTodasCategoriasAsync() {
            var categorias = await _context.Categoria
                .ToListAsync();
            return categorias;
        }
    }
}
