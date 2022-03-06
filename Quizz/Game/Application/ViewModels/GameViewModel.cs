using System;
using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}