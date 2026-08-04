using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using StudioAgenda.Domain.Seguranca.SenhaHash;

namespace StudioAgenda.Infrastructure.Seguranca.SenhaHash;

internal sealed class Argon2SenhaHash : ISenhaHash
{
    private const int DEGREE_OF_PARALLELISM = 1;
    private const int  INTERATONS = 2;
    private const int MEMORY_SIZE = 20*1024;
    private readonly int SALT_SIZE = 16;   
    private readonly int HASH_SIZE = 32;
    
    public string HashSenha(string senha)
    {
        var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);

        var hash = HashSenha(senha, salt);
        
        var bytesCombinados = new byte[hash.Length + salt.Length];
        salt.CopyTo(bytesCombinados, 0);
        salt.CopyTo(bytesCombinados, index: salt.Length);
        
        var senhaCripto = Convert.ToBase64String(bytesCombinados);
        
        return Convert.ToBase64String(bytesCombinados);
    }

    public bool VerificarSenha(string senha, string senhaHash)
    {
       var bytesCombinados = Convert.FromBase64String(senhaHash);
       var salt = new byte[SALT_SIZE];
       var hash = new byte[HASH_SIZE];
       
       Array.Copy(bytesCombinados, salt, SALT_SIZE);
       Array.Copy(bytesCombinados, SALT_SIZE, hash, 0, HASH_SIZE);
       
       var novaHash = HashSenha(senha, salt);

       //para que seja sempre usado o mesmo tempo de "teste" de senha
       return CryptographicOperations.FixedTimeEquals(hash, novaHash);
    }

    private byte[] HashSenha(string senha, byte[] salt)
    {
        var senhaByte = Encoding.UTF8.GetBytes(senha);

        var algoritmoHash = new Argon2d(senhaByte)
        {
            DegreeOfParallelism = DEGREE_OF_PARALLELISM,
            Iterations = INTERATONS,
            MemorySize = MEMORY_SIZE,
            Salt = salt
        };
        
        return algoritmoHash.GetBytes(HASH_SIZE);
    } 
    
}