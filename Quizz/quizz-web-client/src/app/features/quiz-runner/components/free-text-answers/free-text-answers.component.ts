import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { QuizRunnerService } from '../../services/quiz-runner.service';

@Component({
  selector: 'app-free-text-answers',
  templateUrl: './free-text-answers.component.html',
  styleUrls: ['./free-text-answers.component.scss'],
})
export class FreeTextAnswersComponent {
  @Output() answered = new EventEmitter<boolean>();

  answerControl = new FormControl();

  constructor(private readonly quizRunner: QuizRunnerService) {}

  submit(): void {
    this.quizRunner
      .answerQuestion(this.answerControl.value)
      .then((result) => this.answered.emit(result));
  }
}
