import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { AppHttpClient } from '../../../core/services/app-http-client';
import { GameInitializedViewModel } from '../../../shared/models/generated/quiz-runner-generated.models';
import { ParticipantType } from '../../game/models/game.models';

@Injectable({
  providedIn: 'root',
})
export class QuizRunnerService {
  inviteCode$ = new BehaviorSubject<string | undefined>(undefined);
  playerJoined$ = new Subject<string>();

  private readonly apiUrl = '/signalr/runner';
  private connection: HubConnection;

  constructor(private readonly http: AppHttpClient) {}

  connect(): Promise<void> {
    this.connection = this.buildConnection();
    this.configureConnection();
    return this.connection.start();
  }

  initGame(gameId: number): Observable<GameInitializedViewModel> {
    return this.http
      .post<GameInitializedViewModel, any>(`${this.apiUrl}/init`, { gameId })
      .pipe(tap((response) => this.inviteCode$.next(response.inviteCode)));
  }

  tryJoin(inviteCode: string, participantType: ParticipantType): Promise<boolean> {
    return this.connection.invoke('TryJoin', inviteCode, participantType).then((successful) => {
      if (successful) {
        this.inviteCode$.next(inviteCode);
      }
      return new Promise((resolve) => resolve(successful));
    });
  }

  setPlayerName(name: string): Promise<void> {
    return this.connection.invoke('SetPlayerName', this.inviteCode$.value, name);
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
    this.connection.on('PlayerJoined', (name) => this.playerJoined$.next(name));
  }
}
