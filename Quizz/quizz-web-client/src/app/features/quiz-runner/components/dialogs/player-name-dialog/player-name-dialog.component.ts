import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { DialogCloseType } from 'src/app/shared/models/dialog.models';
import { PlayerNameForm } from '../../../models/quiz-runner.models';
import { QuizRunnerService } from '../../../services/quiz-runner.service';

@Component({
  selector: 'app-player-name-dialog',
  templateUrl: './player-name-dialog.component.html',
})
export class PlayerNameDialogComponent {
  formControls: Record<keyof PlayerNameForm, FormControl> = {
    name: new FormControl(null, Validators.required),
  };
  form = new FormGroup(this.formControls);

  constructor(
    private readonly dialogRef: MatDialogRef<PlayerNameDialogComponent>,
    private readonly quizRunner: QuizRunnerService
  ) {}

  submit(): void {
    if (this.form.invalid) {
      return;
    }
    this.quizRunner
      .setPlayerName(this.formControls.name.value)
      .then(() => this.dialogRef.close({ closeType: DialogCloseType.Successful }));
  }
}
