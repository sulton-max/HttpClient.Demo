namespace N2.DelegateHandlers.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
}