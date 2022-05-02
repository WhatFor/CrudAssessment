namespace Crud.Shared.Models;

/// <summary>
/// A response object containing an array of rows, including typical <see cref="UnitResult"/>
/// properties such as <see cref="UnitResult.Successful"/> and <see cref="UnitResult.Errors"/>.
/// Additionally, contains an array of properties useful for pagination.
/// </summary>
/// <typeparam name="T">The type of the rows in <see cref="Rows"/>.</typeparam>
public class PaginatedResult<T> : UnitResult
{
    public IEnumerable<T> Rows { get; set; }
    
    public int Count { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int TotalPagesCount { get; set; }
    
    public static PaginatedResult<T> Successful(IEnumerable<T> rows) => new() { Success = true, Rows = rows };
    public new static PaginatedResult<T> Error(string error) => new() { Success = false, Errors = new List<string> { error }};
    public new static PaginatedResult<T> Error(IEnumerable<string> errors) => new() { Success = false, Errors = errors};
}