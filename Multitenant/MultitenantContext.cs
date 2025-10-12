using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Multitenant
{
    public class MultitenantContext : DbContext
    {
        /// <summary>
        /// The clients DbSet
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        private readonly IConfiguration _configuration;
        private readonly ITenant _tenant;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The database context options instance</param>
        /// <param name="tenant">The tenant instance</param>
        /// <param name="configuration">The configuration instance</param>
        public MultitenantContext(DbContextOptions<MultitenantContext> options, ITenant tenant, IConfiguration configuration)
         : base(options)
        {
            _tenant = tenant;
            _configuration = configuration;
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = _configuration.GetConnectionString("Default")?.Replace("__Suffix__", _tenant?.TenantId.ToString() ?? "default");
            options.UseSqlServer(connectionString);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
                b.Property(x => x.BirthDate).IsRequired();
            });
        }
    }
}
