namespace Crud.Application.BaseQueries;

/// <summary>
/// A base type for all paginated queries through <see cref="MediatR"/>.
/// </summary>
public class BasePaginatedQuery<TReturn> : IRequest<TReturn>
{
    public int PageNumber { get; set; }
    
    public int Count { get; set; }
}