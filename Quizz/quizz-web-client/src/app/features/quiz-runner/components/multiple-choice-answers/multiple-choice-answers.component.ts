import { Component, Input } from '@angular/core';
import { AnswerViewModel } from '../../../../shared/models/generated/game-generated.models';

@Component({
  selector: 'app-multiple-choice-answers',
  templateUrl: './multiple-choice-answers.component.html',
})
export class MultipleChoiceAnswersComponent {
  @Input() answers: AnswerViewModel[];
}
