import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/shared/classes/unsubscribe-on-destroy';
import { DialogCloseType } from 'src/app/shared/models/dialog.models';
import { GameService } from '../../../services/game.service';

@Component({
  selector: 'app-new-game-dialog',
  templateUrl: './new-game-dialog.component.html',
  styleUrls: ['./new-game-dialog.component.scss'],
})
export class NewGameDialogComponent extends UnsubscribeOnDestroy {
  form = new FormControl(null);

  constructor(
    private readonly dialogRef: MatDialogRef<NewGameDialogComponent>,
    private readonly gameService: GameService
  ) {
    super();
  }

  submit(): void {
    this.subscribe(
      this.gameService
        .createGame(this.form.value)
        .pipe(tap(() => this.dialogRef.close({ closeType: DialogCloseType.Successful })))
    );
  }

  cancel(): void {
    this.dialogRef.close({ closeType: DialogCloseType.Canceled });
  }
}
