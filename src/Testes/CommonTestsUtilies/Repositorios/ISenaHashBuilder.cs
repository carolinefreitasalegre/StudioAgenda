using Moq;
using StudioAgenda.Domain.Seguranca.SenhaHash;

namespace CommonTestsUtilies.Repositorios;

public class ISenaHashBuilder
{
    private readonly Mock<ISenhaHash> _mock;

    public ISenaHashBuilder()
    {
        _mock = new Mock<ISenhaHash>();
        _mock.Setup(senhaHash => senhaHash.HashSenha(It.IsAny<string>())).Returns("senha-hash");
    }

    public void VerificarSenha(string senha)
    {
        _mock.Setup(senhaHash => senhaHash.VerificarSenha(senha, It.IsAny<string>())).Returns(true);
    }
    
    public ISenhaHash Build() => _mock.Object;
    
}