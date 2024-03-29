﻿namespace Quizz.Common.ErrorHandling;

public static class ValidationError
{
    // Game
    public static readonly string GameNameRequired = "game_name_required";

    public static readonly string GameIdRequired = "game_id_required";

    public static readonly string GameNotFound = "game_not_found";

    // User
    public static readonly string UserIdRequired = "user_id_required";

    // Questions
    public static readonly string QuestionIdsRequired = "question_ids_required";

    public static readonly string NewQuestionsRequired = "new_questions_required";
}
