import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../components/dialogs/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogData, DialogConfig } from '../models/dialog.models';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  constructor(private readonly matDialogService: MatDialog) {}

  open<DialogT, ConfigDataT>(
    component: ComponentType<DialogT>,
    config?: DialogConfig<ConfigDataT>
  ): MatDialogRef<DialogT> {
    const defaultConfig: DialogConfig = {
      autoFocus: false,
    };
    const dialogRef = this.matDialogService.open(component, { ...defaultConfig, ...config });
    return dialogRef;
  }

  openConfirmDialog(dialogData: ConfirmDialogData): MatDialogRef<ConfirmDialogComponent> {
    const config: DialogConfig<ConfirmDialogData> = {
      data: dialogData,
      autoFocus: false,
    };
    return this.open(ConfirmDialogComponent, config);
  }
}
