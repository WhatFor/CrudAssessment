using System.Net.Mime;
using Crud.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Api.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Fetch a paginated list of contacts.
    /// </summary>
    [HttpGet("paginate")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType(typeof(PaginatedResult<int>))]
    public async Task<IActionResult> PaginateAllAsync()
    {
        return Ok();
    }
}