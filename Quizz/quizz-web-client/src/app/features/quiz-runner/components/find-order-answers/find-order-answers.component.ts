import { Component, Input } from '@angular/core';
import { AnswerViewModel } from '../../../../shared/models/generated/game-generated.models';

@Component({
  selector: 'app-find-order-answers',
  templateUrl: './find-order-answers.component.html',
  styleUrls: ['./find-order-answers.component.scss'],
})
export class FindOrderAnswersComponent {
  @Input() answers: AnswerViewModel[];
}
