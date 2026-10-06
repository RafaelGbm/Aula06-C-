using System.Threading;
using System.Threading.Tasks;
using LojaGraphQL.Api.Data;
using LojaGraphQL.Api.Models;

namespace LojaGraphQL.Api.GraphQL;

public sealed class Mutation
{
    public async Task<CriarProdutoPayload> CriarProdutoAsync(
        CriarProdutoInput input,
        AppDbContext db,
        CancellationToken cancellationToken)
    {
        Produto produto = new()
        {
            Nome = input.Nome,
            Categoria = input.Categoria,
            Preco = input.Preco,
            Estoque = input.Estoque
        };

        db.Produtos.Add(produto);
        await db.SaveChangesAsync(cancellationToken);

        return new CriarProdutoPayload { Produto = produto };
    }
}

public sealed class CriarProdutoInput
{
    public string Nome { get; init; } = string.Empty;

    public string Categoria { get; init; } = string.Empty;

    public decimal Preco { get; init; }

    public int Estoque { get; init; }
}

public sealed class CriarProdutoPayload
{
    public required Produto Produto { get; init; }
}
