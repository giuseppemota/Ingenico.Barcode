using Ingenico.Barcode.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Repository {
    public interface ITagRepository {
        Task<IEnumerable<Tag>> GetTagsByProdutoIdAsync(int produtoId);
    }
}
