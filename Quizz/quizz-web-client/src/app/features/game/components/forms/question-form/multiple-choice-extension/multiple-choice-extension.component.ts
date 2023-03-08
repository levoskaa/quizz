import { Component, Input, OnChanges } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { correctAnswerRequired } from 'src/app/features/game/validators/correctAnswerRequired';

@Component({
  selector: 'app-multiple-choice-extension',
  templateUrl: './multiple-choice-extension.component.html',
  styleUrls: ['./multiple-choice-extension.component.scss'],
})
export class MultipleChoiceExtensionComponent implements OnChanges {
  @Input() form: FormGroup;

  get answerPossibilities(): FormArray {
    return this.form.controls.answerPossibilities as FormArray;
  }

  constructor(private readonly translate: TranslateService) {}

  ngOnChanges(): void {
    this.initAnswerPossibilitiesControl();
  }

  getAnswerForms(): FormGroup[] {
    return this.answerPossibilities.controls as FormGroup[];
  }

  toggleAnswerCorrectness(form: FormGroup): void {
    const currentValue = form.controls.isCorrect.value;
    form.controls.isCorrect.setValue(!currentValue);
  }

  removeAnswer(index: number): void {
    this.answerPossibilities.removeAt(index);
  }

  addAnswer(): void {
    const answer = {
      text: new FormControl(null, Validators.required),
      isCorrect: new FormControl(false, Validators.required),
    };
    this.answerPossibilities.push(new FormGroup(answer));
  }

  getAnswerErrorMessage(): string {
    if (this.answerPossibilities.hasError('minlength')) {
      return this.translate.instant('game.questionForm.answerCountError', { min: 2 });
    }
    if (this.answerPossibilities.hasError('correctAnswerRequired')) {
      return this.translate.instant('game.questionForm.correctAnswerRequired');
    }
    return '';
  }

  private initAnswerPossibilitiesControl(): void {
    if (this.answerPossibilities) {
      this.answerPossibilities.addValidators([
        Validators.required,
        Validators.minLength(2),
        correctAnswerRequired,
      ]);
    }
  }
}
