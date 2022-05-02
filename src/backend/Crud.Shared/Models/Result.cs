namespace Crud.Shared.Models;

/// <summary>
/// A response object containing a success flag, any error messages and returned data.
/// </summary>
/// <typeparam name="T">The type of the returned <see cref="Data"/>.</typeparam>
public class Result<T> : UnitResult
{
    public T Data { get; set; }
    
    public static Result<T> Successful(T data) => new() { Success = true, Data = data };
    public new static Result<T> Error(string error) => new() { Success = false, Errors = new List<string> { error }};
    public new static Result<T> Error(IEnumerable<string> errors) => new() { Success = false, Errors = errors};
}