import { Component, Input, OnChanges } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import {
  QuestionType,
  QuestionViewModel,
} from '../../../../shared/models/generated/game-generated.models';

@Component({
  selector: 'app-questions-list',
  templateUrl: './questions-list.component.html',
  styleUrls: ['./questions-list.component.scss'],
})
export class QuestionsListComponent implements OnChanges {
  @Input() questions: QuestionViewModel[];

  formControls = {
    questions: new FormArray([]),
  };
  form = new FormGroup(this.formControls);

  QuestionType = QuestionType;

  ngOnChanges(): void {
    this.initForm();
  }

  getQuestionControl(index: number): FormControl {
    return this.formControls.questions.at(index) as FormControl;
  }

  private initForm(): void {
    this.formControls.questions.clear();
    for (const question of this.questions) {
      const { index: _, ...newValue } = question;
      this.formControls.questions.push(new FormControl(newValue));
    }
  }
}
