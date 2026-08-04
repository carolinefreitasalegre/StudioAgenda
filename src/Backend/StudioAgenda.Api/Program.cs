using StudioAgenda.Api.Filtros;
using StudioAgenda.Application.DI;
using StudioAgenda.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(opt => opt.Filters.Add<ExceptionFilters>());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//aqiui o autorization
app.MapControllers();


app.Run();

