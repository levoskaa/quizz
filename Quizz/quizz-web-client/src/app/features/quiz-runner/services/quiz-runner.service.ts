import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
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

  private readonly apiUrl = '/signalr/runner';
  private connection: HubConnection;

  constructor(private readonly http: AppHttpClient) {}

  connect(): Promise<void> {
    const connectionBuilder = new HubConnectionBuilder();
    connectionBuilder.withUrl(`${Globals.apiRoot}/hubs/runner`);
    if (!environment.production) {
      connectionBuilder.configureLogging(LogLevel.Information);
    }
    this.connection = connectionBuilder.build();
    return this.connection.start();
  }

  initGame(gameId: number): Observable<GameInitializedViewModel> {
    return this.http
      .post<GameInitializedViewModel, any>(`${this.apiUrl}/init`, { gameId })
      .pipe(tap((response) => this.inviteCode$.next(response.inviteCode)));
  }

  tryJoin(inviteCode: string, participantType: ParticipantType): Promise<boolean> {
    return this.connection.invoke('TryJoin', inviteCode, participantType);
  }
}
