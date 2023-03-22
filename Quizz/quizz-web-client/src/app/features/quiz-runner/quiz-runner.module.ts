import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JoinGamePageComponent } from './pages/join-game-page/join-game-page.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [JoinGamePageComponent],
  imports: [CommonModule, SharedModule],
})
export class QuizRunnerModule {}
