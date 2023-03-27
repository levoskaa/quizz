import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { distinct, switchMap, tap } from 'rxjs/operators';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import { UnsubscribeOnDestroy } from '../../../../shared/classes/unsubscribe-on-destroy';
import { ParticipantType } from '../../../game/models/game.models';
import { Router } from '@angular/router';
import { filterNullAndUndefined } from 'src/app/core/operators/filterNullAndUndefined';
import { Store } from '@ngxs/store';
import { QuizRunnerState } from '../../states/quiz-runner.state';

@Component({
  templateUrl: './game-lobby-page.component.html',
  styleUrls: ['./game-lobby-page.component.scss'],
})
export class GameLobbyPageComponent extends UnsubscribeOnDestroy implements OnInit {
  inviteCode: string;
  players: string[] = [];

  constructor(
    private readonly quizRunner: QuizRunnerService,
    private readonly router: Router,
    private readonly store: Store
  ) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(this.getInviteCode().pipe(switchMap(() => this.joinQuiz())));
    this.subscribe(this.quizRunner.playerJoined$.pipe(tap((name) => this.players.unshift(name))));
    this.subscribe(
      this.quizRunner.gameStarted$.pipe(tap(() => this.router.navigateByUrl('/runner/control')))
    );
  }

  start(): void {
    this.quizRunner.startGame();
  }

  private getInviteCode(): Observable<string> {
    return this.store.select(QuizRunnerState.inviteCode).pipe(
      filterNullAndUndefined(),
      distinct(),
      tap((inviteCode) => (this.inviteCode = inviteCode))
    );
  }

  private joinQuiz(): Promise<boolean> {
    return this.quizRunner.tryJoin(this.inviteCode, ParticipantType.Owner);
  }
}
