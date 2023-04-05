import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Store } from '@ngxs/store';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { filterNullAndUndefined } from '../../../core/operators/filterNullAndUndefined';
import { UnsubscribeOnDestroy } from '../../../shared/classes/unsubscribe-on-destroy';
import { QuestionViewModel } from '../../../shared/models/generated/game-generated.models';
import { environment } from '../../../../environments/environment';
import { AppHttpClient } from '../../../core/services/app-http-client';
import { GameInitializedViewModel } from '../../../shared/models/generated/quiz-runner-generated.models';
import { ParticipantType } from '../../game/models/game.models';
import { QuestionReceived, UpdateInviteCode } from '../actions/quiz-runner.actions';
import { QuizRunnerState } from '../states/quiz-runner.state';

@Injectable({
  providedIn: 'root',
})
export class QuizRunnerService extends UnsubscribeOnDestroy {
  inviteCode: string;
  playerJoined$ = new Subject<string>();
  gameStarted$ = new Subject<void>();

  private readonly apiUrl = '/signalr/runner';
  private connection: HubConnection;

  constructor(private readonly http: AppHttpClient, private readonly store: Store) {
    super();
    this.init();
  }

  connect(): Promise<void> {
    this.connection = this.buildConnection();
    this.configureConnection();
    return this.connection.start();
  }

  initGame(gameId: number): Observable<GameInitializedViewModel> {
    return this.http.post<GameInitializedViewModel, any>(`${this.apiUrl}/init`, { gameId }).pipe(
      tap((response) => {
        this.store.dispatch(new UpdateInviteCode(response.inviteCode ?? ''));
      })
    );
  }

  tryJoin(inviteCode: string, participantType: ParticipantType): Promise<boolean> {
    return this.connection.invoke('TryJoin', inviteCode, participantType).then((successful) => {
      if (successful) {
        this.store.dispatch(new UpdateInviteCode(inviteCode));
      }
      return new Promise((resolve) => resolve(successful));
    });
  }

  setPlayerName(name: string): Promise<void> {
    return this.connection.invoke('SetPlayerName', this.inviteCode, name);
  }

  startGame(): Promise<void> {
    return this.connection.invoke('StartGame', this.inviteCode);
  }

  getCurrentQuestion(): Promise<void> {
    return this.connection.invoke('GetCurrentQuestion', this.inviteCode);
  }

  progressToNextQuestion(): Promise<void> {
    return this.connection.invoke('ProgressToNextQuestion', this.inviteCode);
  }

  answerQuestion(answer: number[] | string | boolean): Promise<boolean> {
    return this.connection.invoke('AnswerQuestion', this.inviteCode, { value: answer });
  }

  private buildConnection(): HubConnection {
    const connectionBuilder = new HubConnectionBuilder();
    connectionBuilder.withUrl(`${Globals.apiRoot}/hubs/runner`).withAutomaticReconnect();
    if (!environment.production) {
      connectionBuilder.configureLogging(LogLevel.Information);
    }
    return connectionBuilder.build();
  }

  private configureConnection(): void {
    this.connection.on('PlayerJoined', (name: string) => this.playerJoined$.next(name));
    this.connection.on('GameStarted', () => this.gameStarted$.next());
    this.connection.on('QuestionReceived', this.onQuestionReceived);
  }

  private onQuestionReceived = (question: QuestionViewModel): void => {
    this.store.dispatch(new QuestionReceived(question));
  };

  private init(): void {
    this.subscribe(this.getInviteCode());
  }

  private getInviteCode(): Observable<string> {
    return this.store.select(QuizRunnerState.inviteCode).pipe(
      filterNullAndUndefined(),
      tap((inviteCode) => (this.inviteCode = inviteCode))
    );
  }
}
