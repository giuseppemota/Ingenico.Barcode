
namespace Ingenico.Barcode.Shared.Responses {
    public class ObterImagemProdutoResponse {
        public string? ImagePath { get; set; }
        public byte[]? ImageData { get; set; } // Se necessário, retornar a imagem como byte array
    }

}
