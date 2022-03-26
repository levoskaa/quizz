import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from 'src/app/core/services/app-http-client';
import { GameViewModelPaginatedItemsViewModel } from 'src/app/shared/models/generated/game-generated.models';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private readonly gameApiUrl = '/game/games';

  constructor(private readonly httpClient: AppHttpClient) {}

  getGames(
    pageSize?: number,
    pageIndex?: number
  ): Observable<GameViewModelPaginatedItemsViewModel> {
    return this.httpClient.get(this.gameApiUrl, { pageSize, pageIndex });
  }

  deleteGame(gameId: number): Observable<void> {
    return this.httpClient.delete(`${this.gameApiUrl}/${gameId}`);
  }
}
