import { Component, OnInit } from '@angular/core';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import { ParticipantResultViewModel } from 'src/app/shared/models/quiz-runner.models';

@Component({
  templateUrl: './player-result-page.component.html',
  styleUrls: ['./player-result-page.component.scss'],
})
export class PlayerResultPageComponent implements OnInit {
  result: ParticipantResultViewModel;
  ranking: number;

  constructor(private readonly quizRunner: QuizRunnerService) {}

  async ngOnInit(): Promise<void> {
    const results = await this.quizRunner.getQuizResults();
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    this.result = results.participantResults.find(
      (result) => result.connectionId === this.quizRunner.connectionId
    )!;
    this.ranking = results.participantResults.indexOf(this.result) + 1;
  }
}
