using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoFranquias.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFornecedoresEChamados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_UnidadesFranqueadas_UnidadeFranqueadaId",
                table: "Chamados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chamados",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "DataFechamento",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Chamados");

            migrationBuilder.RenameTable(
                name: "Chamados",
                newName: "ChamadosSuporte");

            migrationBuilder.RenameColumn(
                name: "RazaoSocial",
                table: "Fornecedores",
                newName: "Nome");

            migrationBuilder.RenameIndex(
                name: "IX_Chamados_UnidadeFranqueadaId",
                table: "ChamadosSuporte",
                newName: "IX_ChamadosSuporte_UnidadeFranqueadaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChamadosSuporte",
                table: "ChamadosSuporte",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChamadosSuporte_UnidadesFranqueadas_UnidadeFranqueadaId",
                table: "ChamadosSuporte",
                column: "UnidadeFranqueadaId",
                principalTable: "UnidadesFranqueadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChamadosSuporte_UnidadesFranqueadas_UnidadeFranqueadaId",
                table: "ChamadosSuporte");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChamadosSuporte",
                table: "ChamadosSuporte");

            migrationBuilder.RenameTable(
                name: "ChamadosSuporte",
                newName: "Chamados");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Fornecedores",
                newName: "RazaoSocial");

            migrationBuilder.RenameIndex(
                name: "IX_ChamadosSuporte_UnidadeFranqueadaId",
                table: "Chamados",
                newName: "IX_Chamados_UnidadeFranqueadaId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFechamento",
                table: "Chamados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Chamados",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chamados",
                table: "Chamados",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_UnidadesFranqueadas_UnidadeFranqueadaId",
                table: "Chamados",
                column: "UnidadeFranqueadaId",
                principalTable: "UnidadesFranqueadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
