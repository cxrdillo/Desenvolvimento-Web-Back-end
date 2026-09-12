using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Repositories
{
    public class RoyaltyRepository : IRoyaltyRepository
    {
        private readonly AppDbContext _context;

        public RoyaltyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Royalty> CalcularRoyaltyAsync(int unidadeId, int mes, int ano)
        {
            var unidade = await _context.UnidadesFranqueadas.FindAsync(unidadeId);
            if (unidade == null) throw new Exception("Unidade não encontrada.");

            // Filtra e soma todas as vendas da unidade correspondentes ao mês e ano de referência informados
            var vendasDoPeriodo = await _context.Vendas
                .Where(v => v.UnidadeFranqueadaId == unidadeId && v.DataVenda.Month == mes && v.DataVenda.Year == ano)
                .ToListAsync();

            decimal faturamentoTotal = vendasDoPeriodo.Sum(v => v.ValorTotal);
            decimal percentual = unidade.PercentualRoyalty; // Ex: 5.0% definido para a franquia
            decimal valorDevido = (faturamentoTotal * percentual) / 100;

            // Verifica se já existe um cálculo fechado de royalties para este mesmo período
            var royaltyExistente = await _context.Royalties
                .FirstOrDefaultAsync(r => r.UnidadeFranqueadaId == unidadeId && r.MesReferencia == mes && r.AnoReferencia == ano);

            if (royaltyExistente != null)
            {
                // Atualiza os valores caso o faturamento tenha sofrido alterações
                royaltyExistente.FaturamentoBase = faturamentoTotal;
                royaltyExistente.PercentualAplicado = percentual;
                royaltyExistente.ValorDevido = valorDevido;
                _context.Royalties.Update(royaltyExistente);
                await _context.SaveChangesAsync();
                return royaltyExistente;
            }

            // Cria um novo registro de apuração de royalties se não houver duplicidade para o período
            var novoRoyalty = new Royalty
            {
                UnidadeFranqueadaId = unidadeId,
                MesReferencia = mes,
                AnoReferencia = ano,
                FaturamentoBase = faturamentoTotal,
                PercentualAplicado = percentual,
                ValorDevido = valorDevido,
                StatusPagamento = "Pendente",
                DataCalculo = DateTime.UtcNow
            };

            await _context.Royalties.AddAsync(novoRoyalty);
            await _context.SaveChangesAsync();

            return novoRoyalty;
        }

        public async Task<IEnumerable<Royalty>> ObterRoyaltiesPorUnidadeAsync(int unidadeId)
        {
            // Retorna o histórico consolidado de royalties apurados para a unidade selecionada
            return await _context.Royalties
                .Where(r => r.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();
        }
    }
}