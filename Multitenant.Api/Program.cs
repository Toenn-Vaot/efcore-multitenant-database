
using Microsoft.EntityFrameworkCore;

namespace Multitenant.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MultitenantContext>();

            //NOTICE: Configure the ITenant service to be resolved from the X-Tenant-Id header
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ITenant>(sp =>
            {
                var tenantIdString = sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Request.Headers["X-Tenant-Id"];
                return (!string.IsNullOrEmpty(tenantIdString) && Guid.TryParse(tenantIdString, out var tenantId) ? new Tenant { TenantId = tenantId } : null)!;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
