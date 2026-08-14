using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoRegistroProfissional : ValidacaoUsuarioBase<RequisicaoRegistrarProfissional>
{
    public ValidacaoRegistroProfissional()
    {
        RuleFor(profissional => profissional.Email).NotEmpty().WithMessage("Campo email deve ser preenchido.");
        RuleFor(profissional => profissional.Email).EmailAddress().WithMessage("Preencha um email válido.");
    }
}