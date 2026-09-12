using GestaoFranquias.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Mapeamento das tabelas (DbSets) para o Entity Framework gerenciar o banco relacional
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UnidadeFranqueada> UnidadesFranqueadas { get; set; }
        public DbSet<ProdutoServico> ProdutosServicos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }
        public DbSet<Royalty> Royalties { get; set; }
        public DbSet<ChamadoSuporte> ChamadosSuporte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Regra de Negócio: Garante restrição de unicidade para e-mail no nível do banco
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Regra de Negócio: Garante restrição de unicidade para CNPJ no nível do banco
            modelBuilder.Entity<UnidadeFranqueada>()
                .HasIndex(u => u.Cnpj)
                .IsUnique();
            
            // Define a precisão dos campos decimais para valores monetários e percentuais (boa prática financeira)
            modelBuilder.Entity<ProdutoServico>().Property(p => p.PrecoBase).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Venda>().Property(v => v.ValorTotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ItemVenda>().Property(i => i.PrecoUnitario).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Royalty>().Property(r => r.FaturamentoBase).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Royalty>().Property(r => r.PercentualAplicado).HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Royalty>().Property(r => r.ValorDevido).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<UnidadeFranqueada>().Property(u => u.PercentualRoyalty).HasColumnType("decimal(5,2)");
        }
    }
}