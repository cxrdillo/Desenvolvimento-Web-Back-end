using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Repositories
{
    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly AppDbContext _context;

        public UnidadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UnidadeFranqueada>> ObterTodasAsync()
        {
            // Retorna a listagem completa de unidades franqueadas cadastradas no banco
            return await _context.UnidadesFranqueadas.ToListAsync();
        }

        public async Task<UnidadeFranqueada?> ObterPorIdAsync(int id)
        {
            // Busca uma unidade pelo ID primário utilizando o rastreamento otimizado do EF Core
            return await _context.UnidadesFranqueadas.FindAsync(id);
        }

        public async Task<UnidadeFranqueada?> ObterPorCnpjAsync(string cnpj)
        {
            // Consulta uma unidade pelo CNPJ para apoiar validações de unicidade antes do cadastro
            return await _context.UnidadesFranqueadas
                .FirstOrDefaultAsync(u => u.Cnpj == cnpj);
        }

        public async Task AdicionarAsync(UnidadeFranqueada unidade)
        {
            // Insere uma nova unidade franqueada e persiste no banco de dados
            await _context.UnidadesFranqueadas.AddAsync(unidade);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(UnidadeFranqueada unidade)
        {
            // Atualiza os dados de uma unidade existente e salva as alterações de forma assíncrona
            _context.UnidadesFranqueadas.Update(unidade);
            await _context.SaveChangesAsync();
        }
    }
}