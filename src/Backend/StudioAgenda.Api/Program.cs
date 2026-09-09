using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using StudioAgenda.Api.Filtros;
using StudioAgenda.Api.Token;
using StudioAgenda.Application.DI;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Profissional;
using StudioAgenda.Domain.Seguranca.Tokens;
using StudioAgenda.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(opt => opt.Filters.Add<ExceptionFilters>());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Entrar usando um Token válido",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });
    
    opt.AddSecurityRequirement(document =>
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer", document),
                []
            }
        };
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IAccessTokenProvider, HttpContextTokenProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtoptions =>
    {
        var signingKey = builder.Configuration.GetValue<string>("Jwt:SigningKey")!;

        jwtoptions.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
            ClockSkew = TimeSpan.Zero
        };

        jwtoptions.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                //profissional
                var profissionalId = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Sub)
                    ?? context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                
                
                if (Guid.TryParse(profissionalId, out var profId) == false)
                {
                    context.Fail("Invalid token object");
                    return;
                }

                var profissionalRepository = context.HttpContext.RequestServices.GetRequiredService<ILeituraProfissionalRepository>();
                
                var existeProfissional = await profissionalRepository.ExisteProfissionalAtivoId(profId);
                if(existeProfissional == false)
                    context.Fail("User not found or inactive");
                
                //cliente
                
                var clienteId = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                                ?? context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (Guid.TryParse(clienteId, out var cliId) == false)
                {
                    context.Fail("Invalid token object");
                    return;
                }
                
                var userRepository = context.HttpContext.RequestServices.GetRequiredService<ILeituraClienteRepository>();

                var existeCliente = await userRepository.ExisteUsuarioAtivoId(cliId);
                if (existeCliente == false)
                    context.Fail("User not found or inactive");
                
                
            },
            
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = context.AuthenticateFailure switch
                {
                    null => new RespostaErroJson(["Esta requisição precisa de um Token válido"]),
                    SecurityTokenExpiredException => new RespostaErroJson("Token expirado", tokenIsExpired: true)
                };
                
                await context.Response.WriteAsJsonAsync(response);
            }
        };
    });

builder.Services.AddHttpContextAccessor();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsEnvironment("Tests"))
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
//aqui o autorization
app.MapControllers();

app.Run();

//partial é para fazer uma fusao com a classe criada e a class gerada 
public partial class Program{}  