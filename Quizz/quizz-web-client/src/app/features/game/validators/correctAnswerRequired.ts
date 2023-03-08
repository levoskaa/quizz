import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { AnswerDto } from '../../../shared/models/generated/game-generated.models';

export const correctAnswerRequired: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const answers: AnswerDto[] = control.value;
  return answers.some((answer) => answer.isCorrect) ? null : { correctAnswerRequired: true };
};
