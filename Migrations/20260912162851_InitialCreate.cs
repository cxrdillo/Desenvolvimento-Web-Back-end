using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoFranquias.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RazaoSocial = table.Column<string>(type: "TEXT", nullable: false),
                    Cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    Contato = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesFranqueadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    Responsavel = table.Column<string>(type: "TEXT", nullable: false),
                    Contato = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativa = table.Column<bool>(type: "INTEGER", nullable: false),
                    PercentualRoyalty = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesFranqueadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    Perfil = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosServicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    PrecoBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosServicos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnidadeFranqueadaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Prioridade = table.Column<string>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamados_UnidadesFranqueadas_UnidadeFranqueadaId",
                        column: x => x.UnidadeFranqueadaId,
                        principalTable: "UnidadesFranqueadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Royalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnidadeFranqueadaId = table.Column<int>(type: "INTEGER", nullable: false),
                    MesReferencia = table.Column<int>(type: "INTEGER", nullable: false),
                    AnoReferencia = table.Column<int>(type: "INTEGER", nullable: false),
                    FaturamentoBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentualAplicado = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ValorDevido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusPagamento = table.Column<string>(type: "TEXT", nullable: false),
                    DataCalculo = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Royalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Royalties_UnidadesFranqueadas_UnidadeFranqueadaId",
                        column: x => x.UnidadeFranqueadaId,
                        principalTable: "UnidadesFranqueadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnidadeFranqueadaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_UnidadesFranqueadas_UnidadeFranqueadaId",
                        column: x => x.UnidadeFranqueadaId,
                        principalTable: "UnidadesFranqueadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnidadeFranqueadaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdutoServicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeMinima = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoques_ProdutosServicos_ProdutoServicoId",
                        column: x => x.ProdutoServicoId,
                        principalTable: "ProdutosServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estoques_UnidadesFranqueadas_UnidadeFranqueadaId",
                        column: x => x.UnidadeFranqueadaId,
                        principalTable: "UnidadesFranqueadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensVendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdutoServicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensVendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensVendas_ProdutosServicos_ProdutoServicoId",
                        column: x => x.ProdutoServicoId,
                        principalTable: "ProdutosServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensVendas_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_UnidadeFranqueadaId",
                table: "Chamados",
                column: "UnidadeFranqueadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoServicoId",
                table: "Estoques",
                column: "ProdutoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_UnidadeFranqueadaId",
                table: "Estoques",
                column: "UnidadeFranqueadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensVendas_ProdutoServicoId",
                table: "ItensVendas",
                column: "ProdutoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensVendas_VendaId",
                table: "ItensVendas",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosServicos_FornecedorId",
                table: "ProdutosServicos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Royalties_UnidadeFranqueadaId",
                table: "Royalties",
                column: "UnidadeFranqueadaId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesFranqueadas_Cnpj",
                table: "UnidadesFranqueadas",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_UnidadeFranqueadaId",
                table: "Vendas",
                column: "UnidadeFranqueadaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");

            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "ItensVendas");

            migrationBuilder.DropTable(
                name: "Royalties");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "ProdutosServicos");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "UnidadesFranqueadas");
        }
    }
}
