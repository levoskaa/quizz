using Dapper;
using MediatR;
using Quizz.Common.Models;
using Quizz.Questions.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.Questions.Application.Commands;

public class GetQuestionsCommandHandler : IRequestHandler<GetQuestionsCommand, IEnumerable<Question>>
{
    private readonly DapperContext dapper;

    public GetQuestionsCommandHandler(DapperContext dapper)
    {
        this.dapper = dapper;
    }

    public async Task<IEnumerable<Question>> Handle(GetQuestionsCommand request, CancellationToken cancellationToken)
    {
        var query = @"SELECT * FROM Question
                     WHERE Id IN @questionIds
                     ORDER BY [Index] ASC;";
        IEnumerable<Question> questions = new List<Question>();
        using (var connection = dapper.CreateConnection())
        {
            questions = await connection.QueryAsync<Question>(query, new { questionIds = request.QuestionIds });
        }
        return questions;
    }
}