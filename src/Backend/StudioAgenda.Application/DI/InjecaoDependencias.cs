using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StudioAgenda.Application.UseCases.Agenda;
using StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;
using StudioAgenda.Application.UseCases.Cliente;
using StudioAgenda.Application.UseCases.Cliente.Perfil;
using StudioAgenda.Application.UseCases.Login;
using StudioAgenda.Application.UseCases.Login.LoginCliente;
using StudioAgenda.Application.UseCases.Profissional.Registrar;
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
        services.AddTransient<IRegistrarProfissional, RegistrarProfissional>();
        services.AddTransient<IRegistrarAgenda, RegistrarAgenda>();
        services.AddTransient<ILoginProfissionalComEmailESenha, LoginProfissionalComEmailESenha>();
        services.AddTransient<ILoginClienteComTelefoneESenha, LoginClienteComTelefoneESenha>();
        services.AddTransient<ILeituraAgendaUseCase, LeituraAgendaUseCase>();
        services.AddTransient<IPerfilClienteUseCase, PerfilClienteUseCase>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<RequisicaoRegistrarCliente>, ValidacaoRegistroCliente>();
        services.AddTransient<IValidator<RequisicaoRegistrarProfissional>, ValidacaoRegistroProfissional>();
        services.AddTransient<IValidator<RequisicaoRegistrarAgenda>, ValidacaoRegistrarAgenda>();
    }
}