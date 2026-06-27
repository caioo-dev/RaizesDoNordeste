using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.CardapioProduto;
using RaizesDoNordeste.Application.DTOs.Responses.CardapioProduto;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Route("api/cardapios/{cardapioId:guid}/produtos")]
[Authorize]
public class CardapioProdutosController(ICardapioProdutoService cardapioProdutoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(CardapioProdutoObterTodosResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorCardapio(Guid cardapioId, CancellationToken cancellationToken)
    {
        CardapioProdutoObterTodosResponse response = await cardapioProdutoService.ObterPorCardapio(cardapioId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CardapioProdutoObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        CardapioProdutoObterPorIdResponse? response = await cardapioProdutoService.ObterPorId(id, cancellationToken);
        if (response is null)
        {
            return NotFound();
        }       

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar(Guid cardapioId, [FromBody] CriarCardapioProdutoRequest request, CancellationToken cancellationToken)
    {
        await cardapioProdutoService.Criar(cardapioId, request, cancellationToken);
        return Created();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarCardapioProdutoRequest request, CancellationToken cancellationToken)
    {
        await cardapioProdutoService.Atualizar(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await cardapioProdutoService.Excluir(id, cancellationToken);
        return NoContent();
    }
}
