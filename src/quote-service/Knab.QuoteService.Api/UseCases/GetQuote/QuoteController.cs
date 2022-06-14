using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Knab.QuoteService.Api.UseCases.GetQuote;

[Route("api/kitten-images/random")]
[ApiController]
public class QuoteController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public IActionResult GetRandomKittenImage()
    {
        return Ok();
    }
}