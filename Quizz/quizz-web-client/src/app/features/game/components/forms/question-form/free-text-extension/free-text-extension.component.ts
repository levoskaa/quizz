import { Component, Input, OnChanges } from '@angular/core';
import { FormGroup, FormArray, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-free-text-extension',
  templateUrl: './free-text-extension.component.html',
  styleUrls: ['./free-text-extension.component.scss'],
})
export class FreeTextExtensionComponent implements OnChanges {
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

  removeAnswer(index: number): void {
    this.answerPossibilities.removeAt(index);
  }

  addAnswer(): void {
    const answer = {
      text: new FormControl(null, Validators.required),
    };
    this.answerPossibilities.push(new FormGroup(answer));
  }

  private initAnswerPossibilitiesControl(): void {
    if (this.answerPossibilities) {
      this.answerPossibilities.addValidators(Validators.required);
    }
  }
}
