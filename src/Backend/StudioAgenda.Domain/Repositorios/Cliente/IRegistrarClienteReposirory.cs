namespace StudioAgenda.Domain.Repositorios;

public interface IRegistrarClienteReposirory
{
    Task RegistrarCliente(Domain.Entidades.Cliente cliente) ;

}