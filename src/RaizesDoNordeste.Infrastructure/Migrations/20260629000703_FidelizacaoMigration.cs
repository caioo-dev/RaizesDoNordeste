using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaizesDoNordeste.Infrastructure.Migrations;

/// <inheritdoc />
public partial class FidelizacaoMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ClientesFidelizacao",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PontosDisponiveis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Nivel = table.Column<int>(type: "int", nullable: false),
                TotalPontosAcumulados = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ConsentimentoLGPD = table.Column<bool>(type: "bit", nullable: false),
                DataAdesao = table.Column<DateTime>(type: "datetime2", nullable: false),
                DataUltimaAtualizacaoNivel = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ClientesFidelizacao", x => x.Id);
                table.ForeignKey(
                    name: "FK_ClientesFidelizacao_Clientes_ClienteId",
                    column: x => x.ClienteId,
                    principalTable: "Clientes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MovimentacoesPontos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClienteFidelizacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Tipo = table.Column<int>(type: "int", nullable: false),
                Pontos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DataMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovimentacoesPontos", x => x.Id);
                table.ForeignKey(
                    name: "FK_MovimentacoesPontos_ClientesFidelizacao_ClienteFidelizacaoId",
                    column: x => x.ClienteFidelizacaoId,
                    principalTable: "ClientesFidelizacao",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ClientesFidelizacao_ClienteId",
            table: "ClientesFidelizacao",
            column: "ClienteId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MovimentacoesPontos_ClienteFidelizacaoId",
            table: "MovimentacoesPontos",
            column: "ClienteFidelizacaoId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MovimentacoesPontos");

        migrationBuilder.DropTable(
            name: "ClientesFidelizacao");
    }
}
