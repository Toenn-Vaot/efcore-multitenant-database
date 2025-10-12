namespace Multitenant
{
    /// <summary>
    /// The tenant interface
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// The tenant identifier
        /// </summary>
        public Guid TenantId { get; set; }
    }
}
