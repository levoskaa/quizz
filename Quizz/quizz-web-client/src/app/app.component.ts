import { Component, OnInit } from '@angular/core';
import { QuizRunnerService } from './features/quiz-runner/services/quiz-runner.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'quizz-web-client';

  constructor(private readonly quizRunner: QuizRunnerService) {}

  ngOnInit(): void {
    this.quizRunner.connect().then(() => console.log('SignalR connection established'));
  }
}
