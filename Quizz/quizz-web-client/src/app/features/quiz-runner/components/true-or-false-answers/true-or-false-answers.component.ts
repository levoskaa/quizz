import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OperationMode } from '../../models/quiz-runner.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';

@Component({
  selector: 'app-true-or-false-answers',
  templateUrl: './true-or-false-answers.component.html',
  styleUrls: ['./true-or-false-answers.component.scss'],
})
export class TrueOrFalseAnswersComponent {
  @Input() questionId: string;
  @Input() mode: OperationMode = 'control';
  @Output() answered = new EventEmitter<boolean>();

  constructor(private readonly quizRunner: QuizRunnerService) {}

  submit(answer: boolean): void {
    this.quizRunner
      .answerQuestion(this.questionId, answer)
      .then((result) => this.answered.emit(result));
  }
}
