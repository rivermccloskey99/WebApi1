using WebApi1.Dtos;

namespace WebApi1.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendCustomerToCommand(CustomerReadDto customerReadDto);
    }
}