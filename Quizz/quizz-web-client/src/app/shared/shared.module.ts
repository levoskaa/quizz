import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { ConfirmDialogComponent } from './components/dialogs/confirm-dialog/confirm-dialog.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { LayoutContainerComponent } from './components/layout/layout-container/layout-container.component';

const components = [LayoutContainerComponent, HeaderComponent, ConfirmDialogComponent];

const commonModules = [
  MatToolbarModule,
  NgScrollbarModule,
  TranslateModule,
  MatMenuModule,
  MatCardModule,
  MatButtonModule,
  MatIconModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  ReactiveFormsModule,
  MatButtonToggleModule,
];

@NgModule({
  declarations: [...components],
  imports: [CommonModule, RouterModule, ...commonModules],
  exports: [...commonModules],
})
export class SharedModule {}
