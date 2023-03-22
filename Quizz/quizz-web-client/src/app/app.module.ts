import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { QuizRunnerModule } from './features/quiz-runner/quiz-runner.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent],
  imports: [AppRoutingModule, CoreModule, SharedModule, QuizRunnerModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
