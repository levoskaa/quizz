import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { NewGameDialogComponent } from './components/dialogs/new-game-dialog/new-game-dialog.component';
import { GameFormComponent } from './components/forms/game-form/game-form.component';
import { GameCardComponent } from './components/game-card/game-card.component';
import { GameRoutingModule } from './game-routing.module';
import { GameDetailPageComponent } from './pages/game-detail-page/game-detail-page.component';
import { GamesPageComponent } from './pages/games-page/games-page.component';

@NgModule({
  declarations: [
    GamesPageComponent,
    GameCardComponent,
    GameDetailPageComponent,
    NewGameDialogComponent,
    GameFormComponent,
  ],
  imports: [CommonModule, GameRoutingModule, SharedModule],
})
export class GameModule {}
