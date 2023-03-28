import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AnswerViewModel } from '../../../../shared/models/generated/game-generated.models';
import { OperationMode } from '../../models/quiz-runner.models';

@Component({
  selector: 'app-multiple-choice-answers',
  templateUrl: './multiple-choice-answers.component.html',
  styleUrls: ['./multiple-choice-answers.component.scss'],
})
export class MultipleChoiceAnswersComponent {
  @Input() answers: AnswerViewModel[];
  @Input() mode: OperationMode = 'control';
  @Output() answered = new EventEmitter<void>();
  selected = Array.from({ length: 4 }, () => false);

  submit(): void {
    // TODO
    this.answered.emit();
  }

  getAnswerBoxClasses(index: number): object {
    return { [`color-${index + 1}`]: true, selected: this.selected[index] };
  }

  toggleSelect(index: number): void {
    this.selected[index] = !this.selected[index];
  }
}
