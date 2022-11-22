using ClientService.Models.Dtos;

namespace ClientService.Repository
{
    public interface IClientRepository
    {
        Task<ClientDto> CreateClient(ClientDto clientDto);
        Task<ClientDto> UpdateClient(ClientDto clientDto);
        Task<bool> DeleteClient(int clientId);
        Task<ClientDto> GetClientByName(string clientName);
    }
}
