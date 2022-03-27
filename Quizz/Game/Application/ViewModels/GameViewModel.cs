using System;
using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.ViewModels
{
    public class GameViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}