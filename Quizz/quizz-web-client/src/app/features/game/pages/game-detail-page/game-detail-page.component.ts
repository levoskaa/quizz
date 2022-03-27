import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { switchMapTo, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/shared/classes/unsubscribe-on-destroy';
import {
  GameViewModel,
  UpdateGameDto,
} from 'src/app/shared/models/generated/game-generated.models';
import { GameService } from '../../services/game.service';

@Component({
  templateUrl: './game-detail-page.component.html',
  styleUrls: ['./game-detail-page.component.scss'],
})
export class GameDetailPageComponent extends UnsubscribeOnDestroy implements OnInit {
  formControls: Record<keyof UpdateGameDto, FormControl> = {
    name: new FormControl(null, Validators.required),
  };
  form = new FormGroup(this.formControls);

  game: GameViewModel;

  constructor(
    private readonly gameService: GameService,
    private readonly route: ActivatedRoute,
    private readonly translate: TranslateService,
    private readonly datePipe: DatePipe
  ) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(this.getGame());
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

  private getGame(): Observable<GameViewModel> {
    const id = this.route.snapshot.params.id;
    return this.gameService.getGame(id).pipe(
      tap((game) => {
        this.game = game;
        this.form.patchValue(game);
      })
    );
  }
}
