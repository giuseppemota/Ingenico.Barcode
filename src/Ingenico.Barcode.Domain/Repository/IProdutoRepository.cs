using Ingenico.Barcode.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Repository {
    public interface IProdutoRepository {
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task AddProdutoAsync(Produto produto);
        Task UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(int id);
    }
}
