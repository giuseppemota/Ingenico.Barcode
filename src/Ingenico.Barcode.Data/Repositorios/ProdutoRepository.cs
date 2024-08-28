﻿
using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ingenico.Barcode.Data.Repositorios {
    public class ProdutoRepository : IProdutoRepository {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ProdutoEntity> AtualizarProdutoAsync(ProdutoEntity produto) {
            _context.Produto.Update(produto);

            return produto;
        }

        public async Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto) {
            _context.Produto.Add(produto);

            return produto;
        }

        public async Task<ProdutoEntity> ExcluirProdutoAsync(ProdutoEntity produto) {
            _context.Produto.Remove(produto);

            return produto;
        }

        public async Task<ProdutoEntity> ObterProdutoAsync(int id) {
            var produto = await _context.Produto
                .FirstOrDefaultAsync(p => p.ProdutoId == id);
            return produto;
        }

        public async Task<List<ProdutoEntity>> ObterTodosProdutosAsync() {
            var produtos = await _context.Produto
                .ToListAsync();
            return produtos;
        }
    }
}