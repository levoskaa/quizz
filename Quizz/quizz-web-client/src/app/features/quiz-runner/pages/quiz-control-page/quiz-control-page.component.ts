import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { Observable, timer } from 'rxjs';
import { map, take, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from '../../../../shared/classes/unsubscribe-on-destroy';
import { filterNullAndUndefined } from '../../../../core/operators/filterNullAndUndefined';
import {
  QuestionType,
  QuestionViewModel,
} from '../../../../shared/models/generated/game-generated.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import { QuizRunnerState } from '../../states/quiz-runner.state';

@Component({
  templateUrl: './quiz-control-page.component.html',
  styleUrls: ['./quiz-control-page.component.scss'],
})
export class QuizControlPageComponent extends UnsubscribeOnDestroy implements OnInit {
  currentQuestion$: Observable<QuestionViewModel>;
  timeRemaining$: Observable<number>;
  QuestionType = QuestionType;

  constructor(private readonly store: Store, private readonly quizRunner: QuizRunnerService) {
    super();
  }

  async ngOnInit(): Promise<void> {
    this.currentQuestion$ = this.store.select(QuizRunnerState.currentQuestion).pipe(
      filterNullAndUndefined(),
      tap((question) => this.startQuestionCountDown(question.timeLimitInSeconds))
    );
    await this.progressAndGetNextQuestion();
  }

  private startQuestionCountDown(seconds: number): void {
    this.timeRemaining$ = timer(0, 1000).pipe(
      map((timePassed) => seconds - timePassed),
      take(seconds + 1),
      tap({
        complete: () => {
          this.quizRunner.displayResults();
          this.startResultCountDown();
        },
      })
    );
  }

  private startResultCountDown(): void {
    setTimeout(() => this.progressAndGetNextQuestion(), 3000);
  }

  private async progressAndGetNextQuestion(): Promise<void> {
    await this.quizRunner.progressToNextQuestion();
    await this.quizRunner.getCurrentQuestion();
  }
}
