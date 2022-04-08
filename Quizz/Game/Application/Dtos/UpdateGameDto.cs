using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.Dtos;

public class UpdateGameDto
{
    [Required]
    public string Name { get; set; }
}
