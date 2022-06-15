using Knab.QuoteService.Application.UseCases.GetQuote;
using Knab.QuoteService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Knab.QuoteService.Api.UseCases.V1.GetQuote;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("v{version:apiVersion}/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly IGetQuoteUseCase _useCase;
    private readonly GetQuotePresenter _presenter;
    
    public QuotesController(IGetQuoteUseCase useCase, GetQuotePresenter presenter)
    {
        _useCase = useCase;
        _presenter = presenter;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetQuotesAsync([FromQuery] GetQuoteRequest request)
    {
        await _useCase.Execute(new Symbol(request.Symbol), _presenter);
        return _presenter.ViewModel;
    }
}