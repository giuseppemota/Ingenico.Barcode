using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class QRCode {
        public int QRCodeId { get; set; }
        public string QRCodeData { get; set; } = default!;
        public byte[] QRCodeBytes { get; set; } = default!;

        // Relacionamento com Produto
        public Produto Produto { get; set; }
    }
}
