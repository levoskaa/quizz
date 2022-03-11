import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GameViewModel } from 'src/app/shared/models/game-generated.models';
import { GameService } from '../../services/game.service';

@Component({
  templateUrl: './games-page.component.html',
})
export class GamesPageComponent implements OnInit {
  games$: Observable<GameViewModel[]>;

  constructor(
    private readonly gameService: GameService,
    private readonly translate: TranslateService,
    private readonly datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.games$ = this.gameService.getGames().pipe(map((response) => response.data));
  }

  getGameSubtitle(game: GameViewModel): string {
    const format = this.translate.instant('common.formats.date.shortWithTime');
    const formattedDate = this.datePipe.transform(game.updatedAt, format);
    return `${this.translate.instant('common.updatedAt')}: ${formattedDate}`;
  }
}
