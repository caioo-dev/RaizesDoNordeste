using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.ValueObjects;

namespace RaizesDoNordeste.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable IDE0022 // Usar o corpo da expressão para método

        modelBuilder.Entity<Usuario>(builder =>
        {
            builder.ToTable("Usuarios");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.SenhaHash).IsRequired().HasMaxLength(256);

            builder.HasMany(u => u.UsuarioUnidades)
                .WithOne(uu => uu.Usuario)
                .HasForeignKey(uu => uu.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Cliente>(builder =>
        {
            builder.ToTable("Clientes");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.Telefone).HasMaxLength(30);
            builder.Property(e => e.Documento).HasMaxLength(50);
            builder.Property(e => e.Observacao).HasMaxLength(1000);
        });

        modelBuilder.Entity<Unidade>(builder =>
        {
            builder.ToTable("Unidades");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NomeFantasia).HasMaxLength(150);
            builder.Property(e => e.RazaoSocial).HasMaxLength(150);
            builder.Property(e => e.Cnpj).HasMaxLength(20);
            builder.Property(e => e.Ie).HasMaxLength(20);

            builder.HasMany(u => u.UsuarioUnidades)
                .WithOne(uu => uu.Unidade)
                .HasForeignKey(uu => uu.UnidadeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ProdutoUnidades)
                .WithOne(pu => pu.Unidade)
                .HasForeignKey(pu => pu.UnidadeID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Produto>(builder =>
        {
            builder.ToTable("Produtos");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            builder.Property(e => e.DataInclusao).IsRequired();

            builder.HasMany(p => p.CardapioProdutos)
                .WithOne(cp => cp.Produto)
                .HasForeignKey(cp => cp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProdutoUnidades)
                .WithOne(pu => pu.Produto)
                .HasForeignKey(pu => pu.ProdutoID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PedidosProdutos)
                .WithOne(pp => pp.Produto)
                .HasForeignKey(pp => pp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProdutoUnidade>(builder =>
        {
            builder.ToTable("ProdutosUnidades");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Produto)
                .WithMany(p => p.ProdutoUnidades)
                .HasForeignKey(e => e.ProdutoID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Unidade)
                .WithMany(u => u.ProdutoUnidades)
                .HasForeignKey(e => e.UnidadeID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.EstoqueDisponivel).HasPrecision(18, 4);
            builder.Property(e => e.EstoqueReservado).HasPrecision(18, 4);
            builder.Property(e => e.DataInclusao).IsRequired();

            builder.HasIndex(e => new { e.ProdutoID, e.UnidadeID }).IsUnique();
        });

        modelBuilder.Entity<Cardapio>(builder =>
        {
            builder.ToTable("Cardapios");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(150);

            builder.HasOne(e => e.Unidade)
                .WithMany()
                .HasForeignKey(e => e.UnidadeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CardapioProdutos)
                .WithOne(cp => cp.Cardapio)
                .HasForeignKey(cp => cp.CardapioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CardapioProduto>(builder =>
        {
            builder.ToTable("CardapiosProdutos");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Cardapio)
                .WithMany(c => c.CardapioProdutos)
                .HasForeignKey(e => e.CardapioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Produto)
                .WithMany(p => p.CardapioProdutos)
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.PrecoVenda).HasPrecision(18, 2);
            builder.Property(e => e.Disponivel).IsRequired();
        });

        modelBuilder.Entity<Pedido>(builder =>
        {
            builder.ToTable("Pedidos");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Unidade)
                .WithMany()
                .HasForeignKey(e => e.UnidadeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Total).HasPrecision(18, 2);

            builder.Property(x => x.Status).HasConversion<string>();

            builder.OwnsOne(x => x.EnderecoEntrega, endereco =>
            {
                endereco.Property(x => x.Logradouro)
                    .HasColumnName("EnderecoEntrega")
                    .IsRequired()
                    .HasMaxLength(300);

                endereco.Property(x => x.Numero)
                    .HasColumnName("NumeroEntrega")
                    .IsRequired()
                    .HasMaxLength(50);

                endereco.Property(x => x.Bairro)
                    .HasColumnName("BairroEntrega")
                    .IsRequired()
                    .HasMaxLength(150);

                endereco.Property(x => x.Cep)
                    .HasColumnName("CepEntrega")
                    .IsRequired()
                    .HasMaxLength(20);
            });

            builder.HasMany(p => p.PedidosProdutos)
                .WithOne(pp => pp.Pedido)
                .HasForeignKey(pp => pp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PedidoPagamento>()
                .WithOne(pp => pp.Pedido)
                .HasForeignKey(pp => pp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PedidoStatusHistorico>()
                .WithOne(h => h.Pedido)
                .HasForeignKey(h => h.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PedidoProduto>(builder =>
        {
            builder.ToTable("PedidosProdutos");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Pedido)
                .WithMany(p => p.PedidosProdutos)
                .HasForeignKey(e => e.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Produto)
                .WithMany(p => p.PedidosProdutos)
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.PrecoUnitario).HasPrecision(18, 2);
            builder.Property(x => x.Quantidade).HasPrecision(18, 4);
            builder.Property(x => x.ValorTotal).HasPrecision(18, 2);
        });

        modelBuilder.Entity<PedidoPagamento>(builder =>
        {
            builder.ToTable("PedidosPagamentos");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Pedido)
                .WithMany()
                .HasForeignKey(e => e.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.Valor).HasPrecision(18, 2);

            builder.Property(e => e.TipoPagamento).HasConversion<string>();
            builder.Property(e => e.Status).HasConversion<string>();
        });

        modelBuilder.Entity<UsuarioUnidade>(builder =>
        {
            builder.ToTable("UsuariosUnidades");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Usuario)
                .WithMany(u => u.UsuarioUnidades)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Unidade)
                .WithMany(u => u.UsuarioUnidades)
                .HasForeignKey(e => e.UnidadeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => new { e.UsuarioId, e.UnidadeId }).IsUnique();
        });

        modelBuilder.Entity<MovimentacaoEstoque>(builder =>
        {
            builder.ToTable("MovimentacoesEstoque");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Produto)
                .WithMany()
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Unidade)
                .WithMany()
                .HasForeignKey(e => e.UnidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Quantidade).HasPrecision(18, 4);
            builder.Property(e => e.SaldoAnterior).HasPrecision(18, 4);
            builder.Property(e => e.SaldoPosterior).HasPrecision(18, 4);
            builder.Property(e => e.TipoMovimentacaoOrigem).HasConversion<string>();
            builder.Property(e => e.Observacao).HasMaxLength(1000);
        });

        modelBuilder.Entity<ReservaEstoque>(builder =>
        {
            builder.ToTable("ReservasEstoque");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Produto)
                .WithMany()
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Unidade)
                .WithMany()
                .HasForeignKey(e => e.UnidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Quantidade).HasPrecision(18, 4);
            builder.Property(e => e.TipoMovimentacaoOrigem).HasConversion<string>();
            builder.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<PedidoStatusHistorico>(builder =>
        {
            builder.ToTable("PedidosStatusHistorico");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Pedido)
                .WithMany()
                .HasForeignKey(e => e.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.StatusAnterior).HasConversion<string>();
            builder.Property(e => e.StatusNovo).HasConversion<string>();
            builder.Property(e => e.Observacao).HasMaxLength(1000);
        });

        modelBuilder.Entity<LogOperacao>(builder =>
        {
            builder.ToTable("LogsOperacao");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Entidade).IsRequired().HasMaxLength(150);
            builder.Property(e => e.DadosJson).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(e => e.Acao).HasConversion<string>();
        });

#pragma warning restore IDE0022 // Usar o corpo da expressão para método
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<ProdutoUnidade> ProdutosUnidades => Set<ProdutoUnidade>();
    public DbSet<Cardapio> Cardapios => Set<Cardapio>();
    public DbSet<CardapioProduto> CardapiosProdutos => Set<CardapioProduto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoProduto> PedidosProdutos => Set<PedidoProduto>();
    public DbSet<PedidoPagamento> PedidosPagamentos => Set<PedidoPagamento>();
    public DbSet<UsuarioUnidade> UsuariosUnidades => Set<UsuarioUnidade>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();
    public DbSet<ReservaEstoque> ReservasEstoque => Set<ReservaEstoque>();
    public DbSet<PedidoStatusHistorico> PedidosStatusHistorico => Set<PedidoStatusHistorico>();
    public DbSet<LogOperacao> LogsOperacao => Set<LogOperacao>();
}
