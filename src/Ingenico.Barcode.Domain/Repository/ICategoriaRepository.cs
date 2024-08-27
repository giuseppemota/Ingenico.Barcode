using Ingenico.Barcode.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Repository {
    public interface ICategoriaRepository {
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
    }
}
