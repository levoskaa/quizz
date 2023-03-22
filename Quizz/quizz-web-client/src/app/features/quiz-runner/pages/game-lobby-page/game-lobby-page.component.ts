import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { distinct, filter, switchMap, tap } from 'rxjs/operators';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import { UnsubscribeOnDestroy } from '../../../../shared/classes/unsubscribe-on-destroy';
import { ParticipantType } from '../../../game/models/game.models';

@Component({
  templateUrl: './game-lobby-page.component.html',
  styleUrls: ['./game-lobby-page.component.scss'],
})
export class GameLobbyPageComponent extends UnsubscribeOnDestroy implements OnInit {
  inviteCode: string;
  players: string[] = [];

  constructor(private quizRunner: QuizRunnerService) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(this.getInviteCode().pipe(switchMap(() => this.joinQuiz())));
    this.subscribe(this.quizRunner.playerJoined$.pipe(tap((name) => this.players.unshift(name))));
  }

  private getInviteCode(): Observable<string> {
    return this.quizRunner.inviteCode$.pipe(
      filter((inviteCode): inviteCode is string => inviteCode !== null && inviteCode !== undefined),
      distinct(),
      tap((inviteCode) => (this.inviteCode = inviteCode))
    );
  }

  private joinQuiz(): Promise<boolean> {
    return this.quizRunner.tryJoin(this.inviteCode, ParticipantType.Owner);
  }
}
