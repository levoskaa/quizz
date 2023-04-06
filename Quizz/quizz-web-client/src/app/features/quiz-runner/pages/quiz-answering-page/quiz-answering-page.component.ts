import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { filterNullAndUndefined } from '../../../../core/operators/filterNullAndUndefined';
import { UnsubscribeOnDestroy } from '../../../../shared/classes/unsubscribe-on-destroy';
import {
  QuestionType,
  QuestionViewModel,
} from '../../../../shared/models/generated/game-generated.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import { QuizRunnerState } from '../../states/quiz-runner.state';

@Component({
  templateUrl: './quiz-answering-page.component.html',
  styleUrls: ['./quiz-answering-page.component.scss'],
})
export class QuizAnsweringPageComponent extends UnsubscribeOnDestroy implements OnInit {
  gameStarted = false;
  questionAnswered = false;
  resultVisible = false;
  answerCorrect: boolean;
  question$: Observable<QuestionViewModel>;
  QuestionType = QuestionType;

  constructor(private readonly quizRunner: QuizRunnerService, private readonly store: Store) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(this.quizRunner.gameStarted$.pipe(tap(() => (this.gameStarted = true))));
    this.subscribe(
      this.quizRunner.displayResults$.pipe(
        tap(() => {
          if (!this.questionAnswered) {
            this.answerCorrect = false;
          }
          this.resultVisible = true;
        })
      )
    );
    this.question$ = this.store.select(QuizRunnerState.currentQuestion).pipe(
      filterNullAndUndefined(),
      tap(() => {
        this.questionAnswered = false;
        this.resultVisible = false;
      })
    );
  }

  onAnswered(answerCorrect: boolean): void {
    this.questionAnswered = true;
    this.answerCorrect = answerCorrect;
  }
}
