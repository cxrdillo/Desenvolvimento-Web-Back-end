using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.DTOs;
using GestaoFranquias.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly AppDbContext _context;

        public VendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Venda> RegistrarVendaAsync(NovaVendaDto dto)
        {
            // Validação: Garante que a venda possui pelo menos um item informado no DTO
            if (!dto.Itens.Any())
                throw new ArgumentException("A venda deve possuir pelo menos um item.");

            var venda = new Venda
            {
                UnidadeFranqueadaId = dto.UnidadeFranqueadaId,
                DataVenda = DateTime.UtcNow,
                ValorTotal = 0
            };

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _context.ProdutosServicos.FindAsync(itemDto.ProdutoServicoId);
                if (produto == null) throw new Exception($"Produto {itemDto.ProdutoServicoId} não encontrado.");

                var valorTotalItem = produto.PrecoBase * itemDto.Quantidade;

                venda.Itens.Add(new ItemVenda
                {
                    ProdutoServicoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.PrecoBase
                });

                venda.ValorTotal += valorTotalItem;

                // Regra de Negócio: Realiza a baixa automática e valida o saldo de estoque antes de efetivar a venda
                var estoque = await _context.Estoques
                    .FirstOrDefaultAsync(e => e.UnidadeFranqueadaId == dto.UnidadeFranqueadaId && e.ProdutoServicoId == produto.Id);
                
                if (estoque == null || estoque.Quantidade < itemDto.Quantidade)
                    throw new InvalidOperationException($"Estoque insuficiente para o produto '{produto.Nome}'.");
                
                estoque.Quantidade -= itemDto.Quantidade;
            }

            // Persiste a venda e atualiza o estoque de forma transacional no banco de dados
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();

            return venda;
        }

        public async Task<IEnumerable<Venda>> ObterVendasPorUnidadeAsync(int unidadeId)
        {
            // Retorna o histórico de vendas com os devidos carregamentos de itens e produtos relacionados
            return await _context.Vendas
                .Include(v => v.Itens)
                .ThenInclude(i => i.ProdutoServico)
                .Where(v => v.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();
        }
    }
}