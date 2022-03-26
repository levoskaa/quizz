import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
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
];

@NgModule({
  declarations: [...components],
  imports: [CommonModule, RouterModule, ...commonModules],
  exports: [...commonModules],
})
export class SharedModule {}
