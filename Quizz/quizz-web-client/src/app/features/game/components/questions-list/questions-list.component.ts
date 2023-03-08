import { Component, Input, OnChanges } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { tap } from 'rxjs/operators';
import {
  QuestionType,
  QuestionViewModel,
} from '../../../../shared/models/generated/game-generated.models';
import { QuestionForm } from '../../models/game.models';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-questions-list',
  templateUrl: './questions-list.component.html',
  styleUrls: ['./questions-list.component.scss'],
})
export class QuestionsListComponent implements OnChanges {
  @Input() gameId: number;
  @Input() questions: QuestionViewModel[];

  formControls = {
    questions: new FormArray([]),
  };
  form = new FormGroup(this.formControls);

  QuestionType = QuestionType;

  constructor(private readonly gameService: GameService) {}

  ngOnChanges(): void {
    this.initForm();
  }

  getQuestionControls(): FormControl[] {
    return this.formControls.questions.controls as FormControl[];
  }

  saveQuestions(): void {
    if (this.form.invalid) {
      return;
    }
    this.gameService
      .updateGameQuestions(this.gameId, this.form.value)
      .pipe(
        tap(() => {
          this.form.markAsUntouched();
          this.form.markAsPristine();
        })
      )
      .subscribe();
  }

  addQuestion(questionType: QuestionType): void {
    const newQuestion: QuestionForm = {
      type: questionType,
      text: '',
      correctAnswer: false,
      answerPossibilities: [],
      timeLimitInSeconds: 30,
    };
    this.formControls.questions.push(new FormControl(newQuestion));
  }

  private initForm(): void {
    this.formControls.questions.clear();
    for (const question of this.questions) {
      const { index: _, ...newValue } = question;
      this.formControls.questions.push(new FormControl(newValue));
    }
  }
}
