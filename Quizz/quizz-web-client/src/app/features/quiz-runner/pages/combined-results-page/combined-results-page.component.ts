import { Component, OnInit } from '@angular/core';
import { QuizRunnerService } from '../../services/quiz-runner.service';
import {
  ParticipantResultViewModel,
  QuizResultsViewModel,
} from '../../../../shared/models/quiz-runner.models';

@Component({
  templateUrl: './combined-results-page.component.html',
  styleUrls: ['./combined-results-page.component.scss'],
})
export class CombinedResultsPageComponent implements OnInit {
  results: QuizResultsViewModel;

  get firstResults(): ParticipantResultViewModel[] {
    return this.results.participantResults.slice(0, 5);
  }

  constructor(private readonly quizRunner: QuizRunnerService) {}

  async ngOnInit(): Promise<void> {
    this.results = await this.quizRunner.getQuizResults();
  }
}
