using System.Net.Mime;
using Crud.Application.Contacts.Queries;
using Crud.Data.Models;
using Crud.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Api.Controllers;

/// <summary>
/// Endpoints to support CRUD operations on the <see cref="Contact"/> entity.
/// </summary>
[ApiController]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
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
    public async Task<GetPaginatedContactsQueryResponse> PaginateAllAsync([FromQuery] GetPaginatedContactsQuery query)
        => await _mediator.Send(query);
}