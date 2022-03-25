import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GameViewModel } from 'src/app/shared/models/game-generated.models';
import { GameService } from '../../services/game.service';

@Component({
  templateUrl: './games-page.component.html',
})
export class GamesPageComponent implements OnInit {
  games$: Observable<GameViewModel[]>;

  constructor(private readonly gameService: GameService) {}

  ngOnInit(): void {
    this.games$ = this.gameService.getGames().pipe(map((response) => response.data));
  }

  onGameEdit(gameId: number): void {
    // TODO
  }

  onGameDelete(gameId: number): void {
    // TODO
  }
}
