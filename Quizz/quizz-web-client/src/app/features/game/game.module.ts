import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { GameCardComponent } from './components/game-card/game-card.component';
import { GameRoutingModule } from './game-routing.module';
import { GamesPageComponent } from './pages/games-page/games-page.component';

@NgModule({
  declarations: [GamesPageComponent, GameCardComponent],
  imports: [CommonModule, GameRoutingModule, SharedModule],
})
export class GameModule {}
