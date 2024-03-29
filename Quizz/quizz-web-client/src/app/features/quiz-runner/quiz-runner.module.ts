import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JoinGamePageComponent } from './pages/join-game-page/join-game-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PlayerNameDialogComponent } from './components/dialogs/player-name-dialog/player-name-dialog.component';
import { GameLobbyPageComponent } from './pages/game-lobby-page/game-lobby-page.component';
import { QuizControlPageComponent } from './pages/quiz-control-page/quiz-control-page.component';
import { NgxsModule } from '@ngxs/store';
import { QuizRunnerState } from './states/quiz-runner.state';
import { QuizAnsweringPageComponent } from './pages/quiz-answering-page/quiz-answering-page.component';
import { MultipleChoiceAnswersComponent } from './components/multiple-choice-answers/multiple-choice-answers.component';
import { TrueOrFalseAnswersComponent } from './components/true-or-false-answers/true-or-false-answers.component';
import { FindOrderAnswersComponent } from './components/find-order-answers/find-order-answers.component';
import { FreeTextAnswersComponent } from './components/free-text-answers/free-text-answers.component';
import { PlayerResultPageComponent } from './pages/player-result-page/player-result-page.component';
import { CombinedResultsPageComponent } from './pages/combined-results-page/combined-results-page.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    GameLobbyPageComponent,
    JoinGamePageComponent,
    PlayerNameDialogComponent,
    QuizControlPageComponent,
    QuizAnsweringPageComponent,
    MultipleChoiceAnswersComponent,
    TrueOrFalseAnswersComponent,
    FindOrderAnswersComponent,
    FreeTextAnswersComponent,
    PlayerResultPageComponent,
    CombinedResultsPageComponent,
  ],
  imports: [CommonModule, RouterModule, SharedModule, NgxsModule.forFeature([QuizRunnerState])],
})
export class QuizRunnerModule {}
