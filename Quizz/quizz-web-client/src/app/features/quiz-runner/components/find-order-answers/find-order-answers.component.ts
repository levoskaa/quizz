import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AnswerViewModel } from '../../../../shared/models/generated/game-generated.models';
import { OperationMode } from '../../models/quiz-runner.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import { shuffle } from '../../../../core/utils/shuffle';

@Component({
  selector: 'app-find-order-answers',
  templateUrl: './find-order-answers.component.html',
  styleUrls: ['./find-order-answers.component.scss'],
})
export class FindOrderAnswersComponent implements OnInit {
  @Input() answers: AnswerViewModel[];
  @Input() questionId: string;
  @Input() mode: OperationMode = 'control';
  @Output() answered = new EventEmitter<boolean>();
  colorMap: { [key: number]: string } = {};

  constructor(private readonly quizRunner: QuizRunnerService) {}

  ngOnInit(): void {
    this.answers.forEach((answer, i) => (this.colorMap[answer.id] = `color-${i + 1}`));
    this.answers = shuffle(this.answers);
  }

  onAnswerDropped(event: CdkDragDrop<AnswerViewModel[]>): void {
    // Need to create a copy for moveItemInArray to operate on, otherwise it throws an error.
    // See: https://stackoverflow.com/questions/64957735/typeerror-cannot-assign-to-read-only-property-0-of-object-object-array-in
    const answersCopy = [...this.answers];
    moveItemInArray(answersCopy, event.previousIndex, event.currentIndex);
    this.answers = answersCopy;
  }

  submit(): void {
    const answer = this.answers.map((answer) => answer.id);
    this.quizRunner
      .answerQuestion(this.questionId, answer)
      .then((result) => this.answered.emit(result));
  }
}
