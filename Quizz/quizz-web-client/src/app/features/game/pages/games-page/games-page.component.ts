import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { map, switchMapTo, tap } from 'rxjs/operators';
import { successfulDialogCloseFilter } from 'src/app/core/utils/successfulDialogCloseFilter';
import { UnsubscribeOnDestroy } from 'src/app/shared/classes/unsubscribe-on-destroy';
import { GameViewModel } from 'src/app/shared/models/generated/game-generated.models';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { GameService } from '../../services/game.service';

@Component({
  templateUrl: './games-page.component.html',
})
export class GamesPageComponent extends UnsubscribeOnDestroy implements OnInit {
  games$: Observable<GameViewModel[]>;

  constructor(
    private readonly gameService: GameService,
    private readonly dialogService: DialogService,
    private readonly translate: TranslateService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {
    super();
  }

  ngOnInit(): void {
    this.getGames();
  }

  onGameDetails(gameId: number): void {
    this.router.navigate([gameId], { relativeTo: this.route });
  }

  onGameDelete(game: GameViewModel): void {
    const dialogRef = this.dialogService.openConfirmDialog({
      title: this.translate.instant('game.deleteDialog.title'),
      text: this.translate.instant('game.deleteDialog.text', { gameName: game.name }),
      isDelete: true,
    });
    this.subscribe(
      dialogRef.afterClosed().pipe(
        successfulDialogCloseFilter(),
        switchMapTo(this.gameService.deleteGame(game.id)),
        tap(() => this.getGames())
      )
    );
  }

  private getGames(): void {
    this.games$ = this.gameService.getGames().pipe(map((response) => response.data));
  }
}
