using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels
{
    public class EntityCreatedViewModel<T>
    {
        [Required]
        public T Id { get; set; }
    }
}