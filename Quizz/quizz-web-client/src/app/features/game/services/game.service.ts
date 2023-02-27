import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from 'src/app/core/services/app-http-client';
import {
  CreateGameDto,
  GameViewModel,
  GameViewModelPaginatedItemsViewModel,
  Int32EntityCreatedViewModel,
  QuestionsListViewModel,
  UpdateGameDto,
} from 'src/app/shared/models/generated/game-generated.models';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private readonly gameApiUrl = '/game/games';

  constructor(private readonly httpClient: AppHttpClient) {}

  createGame(dto: CreateGameDto): Observable<Int32EntityCreatedViewModel> {
    return this.httpClient.post(this.gameApiUrl, dto);
  }

  getGames(
    pageSize?: number,
    pageIndex?: number
  ): Observable<GameViewModelPaginatedItemsViewModel> {
    return this.httpClient.get(this.gameApiUrl, { pageSize, pageIndex });
  }

  getGame(id: number): Observable<GameViewModel> {
    return this.httpClient.get(`${this.gameApiUrl}/${id}`);
  }

  updateGame(id: number, dto: UpdateGameDto): Observable<void> {
    return this.httpClient.put(`${this.gameApiUrl}/${id}`, dto);
  }

  deleteGame(id: number): Observable<void> {
    return this.httpClient.delete(`${this.gameApiUrl}/${id}`);
  }

  getGameQuestions(gameId: number): Observable<QuestionsListViewModel> {
    return this.httpClient.get(`${this.gameApiUrl}/${gameId}/questions`);
  }
}
