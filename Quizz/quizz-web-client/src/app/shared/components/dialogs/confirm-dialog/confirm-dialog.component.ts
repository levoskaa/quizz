import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConfirmDialogData, DialogCloseType } from 'src/app/shared/models/dialog.models';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
})
export class ConfirmDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) readonly dialogData: ConfirmDialogData,
    private readonly dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {}

  confirm(): void {
    this.dialogRef.close({ closeType: DialogCloseType.Successful });
  }

  cancel(): void {
    this.dialogRef.close({ closeType: DialogCloseType.Canceled });
  }
}
