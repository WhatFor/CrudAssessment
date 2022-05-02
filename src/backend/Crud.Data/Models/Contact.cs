namespace Crud.Data.Models;

/// <summary>
/// A DB entity for a contact record.
/// </summary>
public class Contact
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
}