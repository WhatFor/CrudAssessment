namespace Crud.Shared.Models;

/// <summary>
/// A response object including a success flag and any error messages.
/// </summary>
public class UnitResult
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

    public static UnitResult Successful() => new() { Success = true };
    public static UnitResult Error(string error) => new() { Success = false, Errors = new List<string> { error }};
    public static UnitResult Error(IEnumerable<string> errors) => new() { Success = false, Errors = errors};
}