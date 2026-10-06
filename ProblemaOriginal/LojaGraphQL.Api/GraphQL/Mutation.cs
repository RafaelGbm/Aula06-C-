using LojaGraphQL.Api.Models;

namespace LojaGraphQL.Api.GraphQL;

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
