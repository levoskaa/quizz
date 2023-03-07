import { CdkDrag, CdkDragDrop } from '@angular/cdk/drag-drop';
import { Component, Input, OnChanges } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { moveItemInFormArray } from 'src/app/core/utils/move-item-in-form-array';

@Component({
  selector: 'app-find-order-extension',
  templateUrl: './find-order-extension.component.html',
  styleUrls: ['./find-order-extension.component.scss'],
})
export class FindOrderExtensionComponent implements OnChanges {
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
      correctIndex: new FormControl(null, Validators.required),
    };
    this.answerPossibilities.push(new FormGroup(answer));
  }

  onAnswerDropped(event: CdkDragDrop<FormArray>): void {
    moveItemInFormArray(this.answerPossibilities, event.previousIndex, event.currentIndex);
    for (let i = 0; i < this.answerPossibilities.length; i++) {
      const answerForm = this.answerPossibilities.at(i) as FormGroup;
      answerForm.controls.correctIndex.setValue(i + 1);
    }
  }

  private initAnswerPossibilitiesControl(): void {
    if (this.answerPossibilities) {
      this.answerPossibilities.addValidators(Validators.required);
      this.answerPossibilities.addValidators(Validators.minLength(2));
    }
  }
}
