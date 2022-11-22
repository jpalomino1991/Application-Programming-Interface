using MovementService.Models.Dtos;

namespace MovementService.Repository
{
    public interface IMovementRepository
    {
        Task<MovementOutDto> CreateMovement(MovementDto movementDto);
        Task<MovementOutDto> UpdateMovement(MovementDto movementDto);
        Task<bool> DeleteMovement(int movementId);
        Task<MovementOutDto> GetMovementById(int movementId);
        Task<List<MovementOutDto>> GetMovementList(string accountId);
    }
}
