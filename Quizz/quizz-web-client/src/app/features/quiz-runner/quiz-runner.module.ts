import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JoinGamePageComponent } from './pages/join-game-page/join-game-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PlayerNameDialogComponent } from './components/dialogs/player-name-dialog/player-name-dialog.component';
import { GameLobbyPageComponent } from './pages/game-lobby-page/game-lobby-page.component';

@NgModule({
  declarations: [GameLobbyPageComponent, JoinGamePageComponent, PlayerNameDialogComponent],
  imports: [CommonModule, SharedModule],
})
export class QuizRunnerModule {}
