using AutoMapper;
using ClientService.DbContexts;
using ClientService.Models;
using ClientService.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ClientRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ClientDto> CreateClient(ClientDto clientDto)
        {
            Person? person = await _db.People.FirstOrDefaultAsync(p => p.PersonName.Equals(clientDto.PersonName));

            if (person != null)
                return null;

            person = _mapper.Map<Person>(clientDto);
            Client client = _mapper.Map<Client>(clientDto);

            client.PersonId = person.PersonId;
            client.Person = person;
            _db.Clients.Add(client);

            await _db.SaveChangesAsync();
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> UpdateClient(ClientDto clientDto)
        {
            Person? person = await _db.People.FirstOrDefaultAsync(p => p.PersonName.Equals(clientDto.PersonName));
            if (person == null)
                return null;

            Client client = await _db.Clients.Where(c => c.PersonId == person.PersonId).Include(c => c.Person).FirstOrDefaultAsync();

            if(client != null)
            {
                client.ClientPassword = clientDto.ClientPassword;
                client.Person.PersonPhone = clientDto.PersonPhone;
                client.Person.PersonName = clientDto.PersonName;
                client.Person.PersonDirection = clientDto.PersonDirection;
                _db.Clients.Update(client);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<bool> DeleteClient(int clientId)
        {
            Client? client = await _db.Clients.Include(p => p.Person).Where(c => c.ClientId == clientId).FirstOrDefaultAsync();
            if (client == null)
                return false;
            client.ClientState = false;
            _db.Clients.Update(client);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ClientDto> GetClientById(int clientId)
        {
            Client? client = await _db.Clients.Include(p => p.Person).FirstOrDefaultAsync(c => c.ClientId == clientId);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> GetClientByName(string clientName)
        {
            Client? client = await _db.Clients.Include(p => p.Person).FirstOrDefaultAsync(c => c.Person.PersonName.Equals(clientName));
            return _mapper.Map<ClientDto>(client);
        }
    }
}
