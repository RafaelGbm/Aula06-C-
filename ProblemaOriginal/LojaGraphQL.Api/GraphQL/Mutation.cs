using LojaGraphQL.Api.Models;

namespace LojaGraphQL.Api.GraphQL;

public sealed class Mutation
{
    public CriarProdutoPayload CriarProduto(CriarProdutoInput input)
    {
        Produto produto = new()
        {
            Nome = input.Nome,
            Categoria = input.Categoria,
            Preco = input.Preco,
            Estoque = input.Estoque
        };

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
