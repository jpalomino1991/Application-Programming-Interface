using ClientService.Models.Dtos;
using ClientService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using ClientService.Models;

namespace ClientService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository   = clientRepository;
        }

        [HttpGet]
        [Route("{clientName}")]
        public async Task<IActionResult> GetUserByName(string clientName)
        {
            ClientDto client = await _clientRepository.GetClientByName(clientName);
            if(client == null)
            {
                return NotFound("No client found with sent parameters");
            }
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]ClientDto clientDto)
        {
            ClientDto client = await _clientRepository.CreateClient(clientDto);
            if(client == null)
            {
                return BadRequest("Client already exists");
            }
            return StatusCode(StatusCodes.Status201Created, client);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] ClientDto clientDto)
        {
            ClientDto client = await _clientRepository.UpdateClient(clientDto);
            if (client == null)
            {
                return BadRequest("Client doesn't exists");
            }
            return Ok(client);
        }

        [HttpDelete]
        [Route("{clientId}")]
        public async Task<IActionResult> DeleteUser(int clientId)
        {
            bool result = await _clientRepository.DeleteClient(clientId);
            return NoContent();
        }
    }
}
