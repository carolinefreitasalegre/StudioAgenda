using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoUsuarioBase<T> : AbstractValidator<T> where T : UsuarioBaseRequisicao
{
    public ValidacaoUsuarioBase()
    {
        RuleFor(cliente => cliente.Nome).NotEmpty().WithMessage("Campo nome deve ser preenchido.");
        RuleFor(cliente => cliente.Telefone).NotEmpty().WithMessage("Campo telefone deve ser preenchido.");
        
        RuleFor(cliente => cliente.Senha).NotEmpty().WithMessage("Campo senha deve ser preenchido.");

        When(cliente => string.IsNullOrWhiteSpace(cliente.Senha) == false, () =>
        {
            RuleFor(cliente => cliente.Senha)
                .MinimumLength(9).WithMessage("Senhe deve conter pelo menos 9 caracteres.")
                .MaximumLength(100).WithMessage("Senha não pode conter mais de 100 caracteres.")
                .Matches(@"[A-Za-z]").WithMessage("A senha deve conter letras")
                .Matches(@"\d").WithMessage("A senha deve conter ao menos um número")
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("A senha deve conter ao menos um caractere especial");
        });

        When(cliente => string.IsNullOrWhiteSpace(cliente.Telefone) == false, () =>
        {
            RuleFor(cliente => cliente.Telefone)
                .Length(11).WithMessage("Telefone deve conter 11 caracteres.")
                .Matches(@"^\d+$").WithMessage("O campo deve conter apenas números.");
        });
        
        
        //falta validar telefone unico

    }
}