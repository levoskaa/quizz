import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
} from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { tap } from 'rxjs/operators';
import {
  QuestionType,
  QuestionViewModel,
  UpdateGameQuestionsDto,
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
  @Output() questionsUpdated = new EventEmitter<void>();

  formControls = {
    questions: new FormArray([]),
  };
  form = new FormGroup(this.formControls);

  QuestionType = QuestionType;

  constructor(
    private readonly gameService: GameService,
    private readonly cdRef: ChangeDetectorRef
  ) {}

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
    const dto: UpdateGameQuestionsDto = {
      questions: this.formControls.questions.value.map(
        (question: QuestionViewModel, i: number) => ({ ...question, index: i })
      ),
    };
    this.gameService
      .updateGameQuestions(this.gameId, dto)
      .pipe(
        tap(() => {
          this.questionsUpdated.emit();
          this.form.markAsUntouched();
          this.form.markAsPristine();
        })
      )
      .subscribe();
  }

  addQuestion(questionType: QuestionType): void {
    const newQuestion: QuestionForm = {
      type: questionType,
    };
    this.formControls.questions.push(new FormControl(newQuestion));
    this.cdRef.detectChanges();
  }

  deleteQuestion(index: number): void {
    this.formControls.questions.removeAt(index);
    this.form.markAsTouched();
    this.form.markAsDirty();
  }

  private initForm(): void {
    this.formControls.questions.clear();
    for (const question of this.questions) {
      const { index: _, ...newValue } = question;
      this.formControls.questions.push(new FormControl(newValue));
    }
  }
}
