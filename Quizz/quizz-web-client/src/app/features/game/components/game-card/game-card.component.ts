import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { GameViewModel } from 'src/app/shared/models/generated/game-generated.models';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['game-card.component.scss'],
})
export class GameCardComponent {
  @Input() game: GameViewModel;

  @Output() detailsClick = new EventEmitter<number>();
  @Output() deleteClick = new EventEmitter<GameViewModel>();
  @Output() playClick = new EventEmitter<number>();

  constructor(private readonly translate: TranslateService, private readonly datePipe: DatePipe) {}

  getSubtitle(): string {
    const format = this.translate.instant('common.formats.date.shortWithTime');
    const formattedDate = this.datePipe.transform(this.game.updatedAt, format);
    return `${this.translate.instant('common.updatedAt')}: ${formattedDate}`;
  }
}
