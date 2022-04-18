using System.Threading.Tasks;

namespace Quizz.GameService.Infrastructure.Services;

public interface IGameValidatorService
{
    Task CheckGameOwnershipAsync(int gameId, string userId);
}