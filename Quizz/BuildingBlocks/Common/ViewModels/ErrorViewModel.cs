using System.Collections.Generic;

namespace Quizz.Common.ViewModels
{
    public class ErrorViewModel
    {
        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public string StackTrace { get; set; }
    }
}