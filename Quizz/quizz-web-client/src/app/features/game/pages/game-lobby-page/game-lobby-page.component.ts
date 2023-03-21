import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { distinct, filter, switchMap, tap } from 'rxjs/operators';
import { QuizRunnerService } from 'src/app/features/quiz-runner/services/quiz-runner.service';
import { UnsubscribeOnDestroy } from 'src/app/shared/classes/unsubscribe-on-destroy';
import { ParticipantType } from '../../models/game.models';

@Component({
  templateUrl: './game-lobby-page.component.html',
  styleUrls: ['./game-lobby-page.component.scss'],
})
export class GameLobbyPageComponent extends UnsubscribeOnDestroy implements OnInit {
  inviteCode: string;

  constructor(private quizRunner: QuizRunnerService) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(this.getInviteCode().pipe(switchMap(() => this.joinQuiz())));
  }

  private getInviteCode(): Observable<string> {
    return this.quizRunner.inviteCode$.pipe(
      filter((inviteCode): inviteCode is string => inviteCode !== null && inviteCode !== undefined),
      distinct(),
      tap((inviteCode) => (this.inviteCode = inviteCode))
    );
  }

  private joinQuiz(): Promise<void> {
    return this.quizRunner.joinQuiz(this.inviteCode, ParticipantType.Owner);
  }
}
