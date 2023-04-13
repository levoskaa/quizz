import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { combineLatest, Observable } from 'rxjs';
import { map, switchMapTo, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from '../../../../shared/classes/unsubscribe-on-destroy';
import {
  GameViewModel,
  QuestionType,
  QuestionViewModel,
} from '../../../../shared/models/generated/game-generated.models';
import { GameService } from '../../services/game.service';

@Component({
  templateUrl: './game-detail-page.component.html',
})
export class GameDetailPageComponent extends UnsubscribeOnDestroy implements OnInit {
  form = new FormControl(null);
  game: GameViewModel;
  questions: QuestionViewModel[] = [];

  QuestionType = QuestionType;

  constructor(
    private readonly gameService: GameService,
    private readonly route: ActivatedRoute,
    private readonly translate: TranslateService,
    private readonly datePipe: DatePipe
  ) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(combineLatest([this.getGame(), this.getGameQuestions()]));
  }

  getSubtitle(): string {
    const format = this.translate.instant('common.formats.date.shortWithTime');
    const formattedDate = this.datePipe.transform(this.game?.updatedAt, format) ?? '';
    return `${this.translate.instant('common.updatedAt')}: ${formattedDate}`;
  }

  onSaveClick(): void {
    this.subscribe(
      this.gameService.updateGame(this.game.id, this.form.value).pipe(switchMapTo(this.getGame()))
    );
  }

  onQuestionsUpdated(): void {
    this.subscribe(this.getGame());
  }

  private getGame(): Observable<GameViewModel> {
    const id = this.route.snapshot.params.id;
    return this.gameService.getGame(id).pipe(
      tap((game) => {
        this.game = game;
        this.form.reset(game);
      })
    );
  }

  private getGameQuestions(): Observable<QuestionViewModel[]> {
    const gameId = this.route.snapshot.params.id;
    return this.gameService.getGameQuestions(gameId).pipe(
      map((response) => response.data),
      tap((questions) => (this.questions = questions))
    );
  }
}
