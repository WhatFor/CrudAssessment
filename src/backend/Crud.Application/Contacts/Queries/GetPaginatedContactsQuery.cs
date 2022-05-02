namespace Crud.Application.Contacts.Queries;

public sealed class GetPaginatedContactsQuery : BasePaginatedQuery<GetPaginatedContactsQueryResponse> { }

public sealed class GetPaginatedContactsQueryResponse : PaginatedResult<ContactResponse> { }

public sealed class ContactResponse
{
    public int Id { get; set; }

    public string FullName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
}

public class GetPaginatedContactsQueryValidator : AbstractValidator<GetPaginatedContactsQuery>
{
    public GetPaginatedContactsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.")
            .WithSeverity(Severity.Error);
        
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .WithMessage("Count must be greater than 0.")
            .WithSeverity(Severity.Error);
    }
}

public sealed class GetPaginatedContactsQueryHandler
    : IRequestHandler<GetPaginatedContactsQuery, GetPaginatedContactsQueryResponse>
{
    private readonly ILogger<GetPaginatedContactsQueryHandler> _logger;
    private readonly ApplicationDbContext _dbContext;

    public GetPaginatedContactsQueryHandler(
        ILogger<GetPaginatedContactsQueryHandler> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public Task<GetPaginatedContactsQueryResponse> Handle(
        GetPaginatedContactsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}