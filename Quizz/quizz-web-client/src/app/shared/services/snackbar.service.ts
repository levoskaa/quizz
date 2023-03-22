import { Injectable } from '@angular/core';
import {
  MatSnackBar,
  MatSnackBarConfig,
  MatSnackBarRef,
  TextOnlySnackBar,
} from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class SnackbarService {
  constructor(
    private readonly snackbar: MatSnackBar,
    private readonly translate: TranslateService
  ) {}

  openError(message: string, durationSeconds = 3): MatSnackBarRef<TextOnlySnackBar> {
    const config: MatSnackBarConfig = {
      duration: durationSeconds * 1000,
      panelClass: ['error'],
    };
    const action = this.translate.instant('common.close');
    return this.snackbar.open(message, action, config);
  }
}
