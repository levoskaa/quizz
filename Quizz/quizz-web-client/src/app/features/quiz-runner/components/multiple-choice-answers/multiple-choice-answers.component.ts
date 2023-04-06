import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AnswerViewModel } from '../../../../shared/models/generated/game-generated.models';
import { OperationMode } from '../../models/quiz-runner.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';

@Component({
  selector: 'app-multiple-choice-answers',
  templateUrl: './multiple-choice-answers.component.html',
  styleUrls: ['./multiple-choice-answers.component.scss'],
})
export class MultipleChoiceAnswersComponent {
  @Input() answers: AnswerViewModel[];
  @Input() questionId: string;
  @Input() mode: OperationMode = 'control';
  @Output() answered = new EventEmitter<boolean>();
  selected = Array.from({ length: 4 }, () => false);

  constructor(private readonly quizRunner: QuizRunnerService) {}

  submit(): void {
    const answer = this.answers.filter((_, i) => this.selected[i]).map((answer) => answer.id);
    this.quizRunner
      .answerQuestion(this.questionId, answer)
      .then((result) => this.answered.emit(result));
  }

  getAnswerBoxClasses(index: number): object {
    return { [`color-${index + 1}`]: true, selected: this.selected[index] };
  }

  toggleSelect(index: number): void {
    this.selected[index] = !this.selected[index];
  }
}
