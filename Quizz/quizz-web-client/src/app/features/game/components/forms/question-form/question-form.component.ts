import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, Input, NgZone, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { QuestionDto, QuestionType } from 'src/app/shared/models/generated/game-generated.models';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.scss'],
})
export class QuestionFormComponent implements OnInit {
  @Input() questionType: QuestionType;
  @ViewChild('autosize') autosize: CdkTextareaAutosize;

  QuestionType = QuestionType;

  formControls: Record<keyof Omit<QuestionDto, 'index'>, FormControl> = {
    text: new FormControl(null, Validators.required),
    type: new FormControl(null, Validators.required),
    timeLimitInSeconds: new FormControl(30, Validators.required),
    correctAnswer: new FormControl(),
    answerPossibilites: new FormControl(),
  };
  form = new FormGroup(this.formControls);

  constructor(private ngZone: NgZone) {}

  ngOnInit(): void {
    this.formControls.type.setValue(this.questionType);
  }

  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this.ngZone.onStable.pipe(take(1)).subscribe(() => this.autosize.resizeToFitContent(true));
  }
}
