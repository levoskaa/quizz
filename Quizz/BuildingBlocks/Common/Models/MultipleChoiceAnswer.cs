namespace Quizz.Common.Models
{
    public class MultipleChoiceAnswer : Answer
    {
        public bool IsCorrect { get; set; }

        public int DisplayIndex { get; set; }
    }
}