namespace Multitenant;

/// <summary>
/// The client entity
/// </summary>
public class Client
{
    /// <summary>
    /// The identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The birthdate
    /// </summary>
    public DateTime BirthDate { get; set; }
}