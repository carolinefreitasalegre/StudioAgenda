namespace StudioAgenda.Domain.Seguranca.SenhaHash;

public interface ISenhaHash
{
    string HashSenha(string senha);
    bool VerificarSenha(string senha, string senhaHash);
}