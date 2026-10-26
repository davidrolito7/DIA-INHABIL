using ApiDiaInhabil.Data;
using ApiDiaInhabil.Repositories;
using ApiDiaInhabil.Repositories.DiaInhabilSede;
using ApiDiaInhabil.Repositories.DiasInhabiles;
using ApiDiaInhabil.Repositories.Sedes;
using ApiDiaInhabil.Services;
using ApiDiaInhabil.Services.DiaInhabilSede;
using ApiDiaInhabil.Services.DiasInhabiles;
using ApiDiaInhabil.Services.Sedes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString =
	builder.Configuration.GetConnectionString("DefaultConnection")
	?? builder.Configuration["DataBaseConnexions:bdJOP_CentralConnection"]
	?? throw new InvalidOperationException("No se encontro la cadena de conexion.");

builder.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(
			new ApiDiaInhabil.Infrastructure.MexicoCityDateTimeJsonConverter()));

builder.Services.AddDbContext<ApiDiaInhabilContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDiaInhabilRepository, DiaInhabilRepository>();
builder.Services.AddScoped<IDiaInhabilService, DiaInhabilService>();
builder.Services.AddScoped<ISedeService, SedeService>();
builder.Services.AddScoped<ISedeRepository, SedeRepository>();
builder.Services.AddScoped<IDiaInhabilSedeRepository, DiaInhabilSedeRepository>();
builder.Services.AddScoped<IDiaInhabilSedeService, DiaInhabilSedeService>();

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AngularPolicy", policy =>
	{
		policy
			.WithOrigins("http://localhost:4200")
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();

	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint(
			"/swagger/v1/swagger.json",
			"Todos API v1"
		);
	});
}
// Activar CORS
app.UseCors("AngularPolicy");

app.MapControllers();

app.Run();
