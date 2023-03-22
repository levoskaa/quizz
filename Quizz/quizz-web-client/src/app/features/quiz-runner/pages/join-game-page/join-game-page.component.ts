import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { tap } from 'rxjs/operators';
import { successfulDialogCloseFilter } from 'src/app/core/utils/successfulDialogCloseFilter';
import { ParticipantType } from 'src/app/features/game/models/game.models';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { PlayerNameDialogComponent } from '../../components/dialogs/player-name-dialog/player-name-dialog.component';
import { InviteCodeForm } from '../../models/quiz-runner.models';
import { QuizRunnerService } from '../../services/quiz-runner.service';

@Component({
  selector: 'app-join-game-page',
  templateUrl: './join-game-page.component.html',
  styleUrls: ['./join-game-page.component.scss'],
})
export class JoinGamePageComponent {
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
  ) {}

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
        tap(() => {
          // TODO
          console.log('routing...');
        })
      )
      .subscribe();
  }
}
