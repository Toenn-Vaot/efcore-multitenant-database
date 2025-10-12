namespace Multitenant;

/// <summary>
/// The tenant implementation
/// </summary>
public class Tenant : ITenant
{
    /// <inheritdoc/>
    public Guid TenantId { get; set; }
}