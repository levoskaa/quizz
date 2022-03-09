import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { HeaderComponent } from './components/layout/header/header.component';
import { LayoutContainerComponent } from './components/layout/layout-container/layout-container.component';

const components = [LayoutContainerComponent, HeaderComponent];

const commonModules = [MatToolbarModule, MatSidenavModule, NgScrollbarModule, TranslateModule];

@NgModule({
  declarations: [...components],
  imports: [CommonModule, RouterModule, ...commonModules],
  exports: [...commonModules],
})
export class SharedModule {}
