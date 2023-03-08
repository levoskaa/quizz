import { QuestionDto } from 'src/app/shared/models/generated/game-generated.models';

export type QuestionForm = Omit<QuestionDto, 'index'>;
