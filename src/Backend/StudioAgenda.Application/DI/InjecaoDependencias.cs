using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StudioAgenda.Application.UseCases.Cliente;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.DI;

public static class InjecaoDependencias
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddValidators(services);
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IRegistrarCliente, RegistrarCliente>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<RequisicaoRegistrarCliente>, ValidacaoCliente>();
    }
}