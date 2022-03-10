import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { GameRoutingModule } from './game-routing.module';
import { GamesPageComponent } from './pages/games-page/games-page.component';

@NgModule({
  declarations: [GamesPageComponent],
  imports: [CommonModule, GameRoutingModule, SharedModule],
})
export class GameModule {}
