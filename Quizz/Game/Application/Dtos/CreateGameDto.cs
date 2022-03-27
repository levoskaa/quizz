using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.Dtos
{
    public class CreateGameDto
    {
        [Required]
        public string Name { get; set; }
    }
}