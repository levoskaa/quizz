import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../components/dialogs/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogData } from '../models/dialog.models';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  constructor(private readonly matDialogService: MatDialog) {}

  openConfirmDialog(dialogData: ConfirmDialogData): MatDialogRef<ConfirmDialogComponent> {
    const dialogRef = this.matDialogService.open(ConfirmDialogComponent, {
      data: dialogData,
      autoFocus: false,
    });
    return dialogRef;
  }
}
