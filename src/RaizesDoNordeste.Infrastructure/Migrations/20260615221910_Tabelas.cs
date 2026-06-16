using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaizesDoNordeste.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Tabelas : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clientes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Telefone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                Documento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                Ativo = table.Column<bool>(type: "bit", nullable: false),
                DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Clientes", x => x.Id));

        migrationBuilder.CreateTable(
            name: "LogsOperacao",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Entidade = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                EntidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Acao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DadosJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DataOperacao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LogsOperacao", x => x.Id);
                table.ForeignKey(
                    name: "FK_LogsOperacao_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Produtos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Ativo = table.Column<bool>(type: "bit", nullable: false),
                DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Produtos", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Unidades",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NomeFantasia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                RazaoSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                CNPJ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                IE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Ativo = table.Column<bool>(type: "bit", nullable: false),
                DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Unidades", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Cardapios",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Ativo = table.Column<bool>(type: "bit", nullable: false),
                DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cardapios", x => x.Id);
                table.ForeignKey(
                    name: "FK_Cardapios_Unidades_UnidadeId",
                    column: x => x.UnidadeId,
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MovimentacoesEstoque",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DocumentoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TipoMovimentacaoOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Quantidade = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                DataMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                SaldoAnterior = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                SaldoPosterior = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovimentacoesEstoque", x => x.Id);
                table.ForeignKey(
                    name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                    column: x => x.ProdutoId,
                    principalTable: "Produtos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_MovimentacoesEstoque_Unidades_UnidadeId",
                    column: x => x.UnidadeId,
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_MovimentacoesEstoque_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Pedidos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EnderecoEntrega = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                NumeroEntrega = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                BairroEntrega = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                CepEntrega = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pedidos", x => x.Id);
                table.ForeignKey(
                    name: "FK_Pedidos_Clientes_ClienteId",
                    column: x => x.ClienteId,
                    principalTable: "Clientes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Pedidos_Unidades_UnidadeId",
                    column: x => x.UnidadeId,
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Pedidos_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProdutosUnidades",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProdutoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UnidadeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EstoqueDisponivel = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                EstoqueReservado = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                Ativo = table.Column<bool>(type: "bit", nullable: false),
                DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProdutosUnidades", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProdutosUnidades_Produtos_ProdutoID",
                    column: x => x.ProdutoID,
                    principalTable: "Produtos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProdutosUnidades_Unidades_UnidadeID",
                    column: x => x.UnidadeID,
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ReservasEstoque",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DocumentoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TipoMovimentacaoOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Quantidade = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReservasEstoque", x => x.Id);
                table.ForeignKey(
                    name: "FK_ReservasEstoque_Produtos_ProdutoId",
                    column: x => x.ProdutoId,
                    principalTable: "Produtos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ReservasEstoque_Unidades_UnidadeId",
                    column: x => x.UnidadeId,
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ReservasEstoque_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UsuariosUnidades",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuariosUnidades", x => x.Id);
                table.ForeignKey(
                    name: "FK_UsuariosUnidades_Unidades_UnidadeId",
                    column: x => x.UnidadeId,
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UsuariosUnidades_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CardapiosProdutos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CardapioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                Disponivel = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CardapiosProdutos", x => x.Id);
                table.ForeignKey(
                    name: "FK_CardapiosProdutos_Cardapios_CardapioId",
                    column: x => x.CardapioId,
                    principalTable: "Cardapios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CardapiosProdutos_Produtos_ProdutoId",
                    column: x => x.ProdutoId,
                    principalTable: "Produtos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PedidosPagamentos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TipoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PedidosPagamentos", x => x.Id);
                table.ForeignKey(
                    name: "FK_PedidosPagamentos_Pedidos_PedidoId",
                    column: x => x.PedidoId,
                    principalTable: "Pedidos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PedidosProdutos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Quantidade = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PedidosProdutos", x => x.Id);
                table.ForeignKey(
                    name: "FK_PedidosProdutos_Pedidos_PedidoId",
                    column: x => x.PedidoId,
                    principalTable: "Pedidos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PedidosProdutos_Produtos_ProdutoId",
                    column: x => x.ProdutoId,
                    principalTable: "Produtos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PedidosStatusHistorico",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StatusAnterior = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StatusNovo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PedidosStatusHistorico", x => x.Id);
                table.ForeignKey(
                    name: "FK_PedidosStatusHistorico_Pedidos_PedidoId",
                    column: x => x.PedidoId,
                    principalTable: "Pedidos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PedidosStatusHistorico_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Usuarios_Email",
            table: "Usuarios",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Cardapios_UnidadeId",
            table: "Cardapios",
            column: "UnidadeId");

        migrationBuilder.CreateIndex(
            name: "IX_CardapiosProdutos_CardapioId",
            table: "CardapiosProdutos",
            column: "CardapioId");

        migrationBuilder.CreateIndex(
            name: "IX_CardapiosProdutos_ProdutoId",
            table: "CardapiosProdutos",
            column: "ProdutoId");

        migrationBuilder.CreateIndex(
            name: "IX_LogsOperacao_UsuarioId",
            table: "LogsOperacao",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_MovimentacoesEstoque_ProdutoId",
            table: "MovimentacoesEstoque",
            column: "ProdutoId");

        migrationBuilder.CreateIndex(
            name: "IX_MovimentacoesEstoque_UnidadeId",
            table: "MovimentacoesEstoque",
            column: "UnidadeId");

        migrationBuilder.CreateIndex(
            name: "IX_MovimentacoesEstoque_UsuarioId",
            table: "MovimentacoesEstoque",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_Pedidos_ClienteId",
            table: "Pedidos",
            column: "ClienteId");

        migrationBuilder.CreateIndex(
            name: "IX_Pedidos_UnidadeId",
            table: "Pedidos",
            column: "UnidadeId");

        migrationBuilder.CreateIndex(
            name: "IX_Pedidos_UsuarioId",
            table: "Pedidos",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidosPagamentos_PedidoId",
            table: "PedidosPagamentos",
            column: "PedidoId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidosProdutos_PedidoId",
            table: "PedidosProdutos",
            column: "PedidoId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidosProdutos_ProdutoId",
            table: "PedidosProdutos",
            column: "ProdutoId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidosStatusHistorico_PedidoId",
            table: "PedidosStatusHistorico",
            column: "PedidoId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidosStatusHistorico_UsuarioId",
            table: "PedidosStatusHistorico",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_ProdutosUnidades_ProdutoID_UnidadeID",
            table: "ProdutosUnidades",
            columns: ["ProdutoID", "UnidadeID"],
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProdutosUnidades_UnidadeID",
            table: "ProdutosUnidades",
            column: "UnidadeID");

        migrationBuilder.CreateIndex(
            name: "IX_ReservasEstoque_ProdutoId",
            table: "ReservasEstoque",
            column: "ProdutoId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservasEstoque_UnidadeId",
            table: "ReservasEstoque",
            column: "UnidadeId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservasEstoque_UsuarioId",
            table: "ReservasEstoque",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosUnidades_UnidadeId",
            table: "UsuariosUnidades",
            column: "UnidadeId");

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosUnidades_UsuarioId_UnidadeId",
            table: "UsuariosUnidades",
            columns: ["UsuarioId", "UnidadeId"],
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CardapiosProdutos");

        migrationBuilder.DropTable(
            name: "LogsOperacao");

        migrationBuilder.DropTable(
            name: "MovimentacoesEstoque");

        migrationBuilder.DropTable(
            name: "PedidosPagamentos");

        migrationBuilder.DropTable(
            name: "PedidosProdutos");

        migrationBuilder.DropTable(
            name: "PedidosStatusHistorico");

        migrationBuilder.DropTable(
            name: "ProdutosUnidades");

        migrationBuilder.DropTable(
            name: "ReservasEstoque");

        migrationBuilder.DropTable(
            name: "UsuariosUnidades");

        migrationBuilder.DropTable(
            name: "Cardapios");

        migrationBuilder.DropTable(
            name: "Pedidos");

        migrationBuilder.DropTable(
            name: "Produtos");

        migrationBuilder.DropTable(
            name: "Clientes");

        migrationBuilder.DropTable(
            name: "Unidades");

        migrationBuilder.DropIndex(
            name: "IX_Usuarios_Email",
            table: "Usuarios");
    }
}
