using Dapper;
using Quizz.Common.ErroHandling;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Data;
using System.Threading.Tasks;

namespace Quizz.GameService.Infrastructure.Services;

public class GameValidatorService : IGameValidatorService
{
    private readonly DapperContext dapper;

    public GameValidatorService(DapperContext dapper)
    {
        this.dapper = dapper;
    }

    public async Task CheckGameOwnershipAsync(int gameId, string userId)
    {
        var query = @"SELECT COUNT(*) FROM Game
                      WHERE OwnerId=@userId AND Id=@gameId;";
        int queryResult = 0;
        using (var connection = dapper.CreateConnection())
        {
            queryResult = await connection.ExecuteScalarAsync<int>(query, new { gameId, userId });
        }
        if (queryResult == 0)
        {
            throw new EntityNotFoundException($"Game with id {gameId} not found", ValidationError.GameNotFound);
        }
    }
}