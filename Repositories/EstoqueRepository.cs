using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly AppDbContext _context;

        public EstoqueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estoque>> ObterEstoqueBaixoAsync(int unidadeId)
        {
            // Regra: Consulta itens cuja quantidade atual está abaixo do estoque mínimo definido
            return await _context.Estoques
                .Include(e => e.ProdutoServico)
                .Where(e => e.UnidadeFranqueadaId == unidadeId && e.Quantidade < e.QuantidadeMinima)
                .ToListAsync();
        }

        public async Task MovimentarEstoqueAsync(int unidadeId, int produtoId, int quantidade)
        {
            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.UnidadeFranqueadaId == unidadeId && e.ProdutoServicoId == produtoId);

            if (estoque == null)
            {
                // Validação: Impede saída de estoque para um produto que nunca foi cadastrado/movimentado na unidade
                if (quantidade < 0) 
                    throw new InvalidOperationException("Não é possível realizar saída de um produto sem estoque.");
                
                estoque = new Estoque 
                { 
                    UnidadeFranqueadaId = unidadeId, 
                    ProdutoServicoId = produtoId, 
                    Quantidade = quantidade,
                    QuantidadeMinima = 5 // Limite padrão para alertas de estoque crítico
                };
                await _context.Estoques.AddAsync(estoque);
            }
            else
            {
                // Regra de Negócio Crítica: O saldo do estoque não pode ficar negativo em nenhuma hipótese
                if (estoque.Quantidade + quantidade < 0)
                    throw new InvalidOperationException("O saldo de estoque não pode ficar negativo.");
                
                estoque.Quantidade += quantidade;
                _context.Estoques.Update(estoque);
            }
            
            // Persiste as alterações com segurança no banco de dados
            await _context.SaveChangesAsync();
        }
    }
}