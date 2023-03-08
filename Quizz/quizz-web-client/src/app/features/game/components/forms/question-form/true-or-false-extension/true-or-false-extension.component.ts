import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-true-or-false-extension',
  templateUrl: './true-or-false-extension.component.html',
})
export class TrueOrFalseExtensionComponent implements OnInit {
  @Input() form: FormGroup;

  get correctAnswerControl(): FormControl {
    return this.form.controls.correctAnswer as FormControl;
  }

  ngOnInit(): void {
    this.correctAnswerControl.addValidators(Validators.required);
  }
}
