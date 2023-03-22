import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { map, switchMapTo, tap } from 'rxjs/operators';
import { successfulDialogCloseFilter } from '../../../../core/utils/successfulDialogCloseFilter';
import { QuizRunnerService } from '../../../quiz-runner/services/quiz-runner.service';
import { GameViewModel } from '../../../../shared/models/generated/game-generated.models';
import { DialogService } from '../../../../shared/services/dialog.service';
import { NewGameDialogComponent } from '../../components/dialogs/new-game-dialog/new-game-dialog.component';
import { GameService } from '../../services/game.service';

@Component({
  templateUrl: './games-page.component.html',
  styleUrls: ['./games-page.component.scss'],
})
export class GamesPageComponent implements OnInit {
  games$: Observable<GameViewModel[]>;

  constructor(
    private readonly gameService: GameService,
    private readonly dialogService: DialogService,
    private readonly translate: TranslateService,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly runnerService: QuizRunnerService
  ) {}

  ngOnInit(): void {
    this.getGames();
  }

  onGameAdd(): void {
    const dialogRef = this.dialogService.open(NewGameDialogComponent);
    dialogRef
      .afterClosed()
      .pipe(
        successfulDialogCloseFilter(),
        tap(() => this.getGames())
      )
      .subscribe();
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
    dialogRef
      .afterClosed()
      .pipe(
        successfulDialogCloseFilter(),
        switchMapTo(this.gameService.deleteGame(game.id)),
        tap(() => this.getGames())
      )
      .subscribe();
  }

  onGameLaunch(gameId: number): void {
    this.runnerService
      .initGame(gameId)
      .pipe(tap(() => this.router.navigateByUrl(`/runner/${gameId}`)))
      .subscribe();
  }

  private getGames(): void {
    this.games$ = this.gameService.getGames().pipe(map((response) => response.data));
  }
}
