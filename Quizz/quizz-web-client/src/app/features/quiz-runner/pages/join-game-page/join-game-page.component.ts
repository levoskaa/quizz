import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { tap } from 'rxjs/operators';
import { successfulDialogCloseFilter } from '../../../../core/utils/successfulDialogCloseFilter';
import { ParticipantType } from '../../../../features/game/models/game.models';
import { UnsubscribeOnDestroy } from '../../../../shared/classes/unsubscribe-on-destroy';
import { DialogService } from '../../../../shared/services/dialog.service';
import { SnackbarService } from '../../../../shared/services/snackbar.service';
import { PlayerNameDialogComponent } from '../../components/dialogs/player-name-dialog/player-name-dialog.component';
import { InviteCodeForm } from '../../models/quiz-runner.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';

@Component({
  templateUrl: './join-game-page.component.html',
  styleUrls: ['./join-game-page.component.scss'],
})
export class JoinGamePageComponent extends UnsubscribeOnDestroy implements OnInit {
  formControls: Record<keyof InviteCodeForm, FormControl> = {
    inviteCode: new FormControl(null, [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(8),
    ]),
  };
  form = new FormGroup(this.formControls);

  constructor(
    private readonly quizRunner: QuizRunnerService,
    private readonly snackbar: SnackbarService,
    private readonly translate: TranslateService,
    private readonly dialogService: DialogService,
    private readonly router: Router
  ) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(
      this.quizRunner.gameStarted$.pipe(tap(() => this.router.navigateByUrl('/answering')))
    );
  }

  transformInput(): void {
    this.formControls.inviteCode.patchValue(this.formControls.inviteCode.value.toUpperCase());
  }

  async submit(): Promise<void> {
    if (this.form.invalid) {
      return;
    }
    const successful = await this.quizRunner.tryJoin(
      this.formControls.inviteCode.value,
      ParticipantType.Player
    );
    if (!successful) {
      const error = this.translate.instant('errors.invalidInviteCode');
      this.snackbar.openError(error, 3);
      return;
    }
    this.openNameDialog();
  }

  openNameDialog(): void {
    const dialogRef = this.dialogService.open(PlayerNameDialogComponent);
    dialogRef
      .afterClosed()
      .pipe(
        successfulDialogCloseFilter(),
        tap(() => this.router.navigateByUrl('/answering'))
      )
      .subscribe();
  }
}
