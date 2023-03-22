import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { NewGameDialogComponent } from './components/dialogs/new-game-dialog/new-game-dialog.component';
import { GameFormComponent } from './components/forms/game-form/game-form.component';
import { QuestionFormComponent } from './components/forms/question-form/question-form.component';
import { TrueOrFalseExtensionComponent } from './components/forms/question-form/true-or-false-extension/true-or-false-extension.component';
import { GameCardComponent } from './components/game-card/game-card.component';
import { QuestionsListComponent } from './components/questions-list/questions-list.component';
import { GameRoutingModule } from './game-routing.module';
import { GameDetailPageComponent } from './pages/game-detail-page/game-detail-page.component';
import { GamesPageComponent } from './pages/games-page/games-page.component';
import { MultipleChoiceExtensionComponent } from './components/forms/question-form/multiple-choice-extension/multiple-choice-extension.component';
import { FreeTextExtensionComponent } from './components/forms/question-form/free-text-extension/free-text-extension.component';
import { FindOrderExtensionComponent } from './components/forms/question-form/find-order-extension/find-order-extension.component';

@NgModule({
  declarations: [
    GamesPageComponent,
    GameCardComponent,
    GameDetailPageComponent,
    NewGameDialogComponent,
    GameFormComponent,
    QuestionsListComponent,
    QuestionFormComponent,
    TrueOrFalseExtensionComponent,
    MultipleChoiceExtensionComponent,
    FreeTextExtensionComponent,
    FindOrderExtensionComponent,
  ],
  imports: [CommonModule, GameRoutingModule, SharedModule],
})
export class GameModule {}
