import { Component, Input, OnChanges } from '@angular/core';
import { FormArray, FormGroup, Validators } from '@angular/forms';

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

  private initAnswerPossibilitiesControl(): void {
    if (this.answerPossibilities) {
      this.answerPossibilities.addValidators(Validators.required);
      this.answerPossibilities.addValidators(Validators.minLength(2));
    }
  }
}
