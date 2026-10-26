using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ApiDiaInhabil.Data;

public class ApiDiaInhabilContextFactory : IDesignTimeDbContextFactory<ApiDiaInhabilContext>
{
	public ApiDiaInhabilContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false)
			.AddJsonFile("appsettings.Development.json", optional: true)
			.Build();

		var connectionString =
			configuration.GetConnectionString("DefaultConnection")
			?? configuration["DataBaseConnexions:bdJOP_CentralConnection"]
			?? throw new InvalidOperationException("No se encontró la cadena de conexión.");

		var optionsBuilder = new DbContextOptionsBuilder<ApiDiaInhabilContext>();
		optionsBuilder.UseSqlServer(connectionString);

		return new ApiDiaInhabilContext(optionsBuilder.Options);
	}
}
