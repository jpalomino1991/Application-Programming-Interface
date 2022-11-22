using Microsoft.AspNetCore.Mvc;
using MovementService.Models.Dtos;
using MovementService.Repository;

namespace MovementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovementController : Controller
    {
        private IMovementRepository _movementRepository;

        public MovementController(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }
        [HttpGet]
        [Route("accountId")]
        public async Task<IActionResult> GetMovementList(string accountId)
        {
            List<MovementOutDto> movements = await _movementRepository.GetMovementList(accountId);
            if (movements.Count == 0)
            {
                return NotFound("No movements found with sent parameters");
            }
            return Ok(movements);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovement([FromBody] MovementDto movementDto)
        {
            MovementDto movement = await _movementRepository.CreateMovement(movementDto);
            if (movement == null)
            {
                return BadRequest("Movement already exists");
            }
            return StatusCode(StatusCodes.Status201Created, movement);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] MovementDto movementDto)
        {
            MovementDto movement = await _movementRepository.CreateMovement(movementDto);
            if (movement == null)
            {
                return BadRequest("Movement already exists");
            }
            return StatusCode(StatusCodes.Status201Created, movement);
        }

        [HttpDelete]
        [Route("{movementId}")]
        public async Task<IActionResult> DeleteMovement(int movementId)
        {
            bool result = await _movementRepository.DeleteMovement(movementId);
            return NoContent();
        }
    }
}
