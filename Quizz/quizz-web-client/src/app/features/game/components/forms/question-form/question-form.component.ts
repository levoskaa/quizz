import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, Input, NgZone, OnInit, ViewChild } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormArray,
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator,
  Validators,
} from '@angular/forms';
import { take } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from '../../../../../shared/classes/unsubscribe-on-destroy';
import {
  AnswerDto,
  QuestionType,
} from '../../../../../shared/models/generated/game-generated.models';
import { QuestionForm } from '../../../models/game.models';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: QuestionFormComponent,
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: QuestionFormComponent,
    },
  ],
})
export class QuestionFormComponent
  extends UnsubscribeOnDestroy
  implements OnInit, ControlValueAccessor, Validator
{
  @Input() questionType: QuestionType;
  @ViewChild('autosize') autosize: CdkTextareaAutosize;

  QuestionType = QuestionType;

  formControls: Record<keyof QuestionForm, AbstractControl> = {
    text: new FormControl(null, Validators.required),
    type: new FormControl(null, Validators.required),
    timeLimitInSeconds: new FormControl(30, Validators.required),
    correctAnswer: new FormControl(),
    answerPossibilities: new FormArray([]),
  };
  form = new FormGroup(this.formControls);

  // eslint-disable-next-line @typescript-eslint/no-empty-function
  onChange = (_question?: QuestionForm) => {};
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  onTouched = () => {};
  touched = false;

  constructor(private ngZone: NgZone) {
    super();
  }

  ngOnInit(): void {
    this.subscribe(this.form.valueChanges, {
      next: () => {
        this.markAsTouched();
        this.onChange(this.form.value);
      },
    });
    this.formControls.type.setValue(this.questionType);
  }

  writeValue(question: QuestionForm): void {
    const answersFormArray = this.formControls.answerPossibilities as FormArray;
    answersFormArray.clear();
    question.answerPossibilities?.sort((a, b) => (a.correctIndex ?? 0) - (b.correctIndex ?? 0));
    for (let i = 0; i < (question.answerPossibilities?.length ?? 0); i++) {
      const answerForm: Record<keyof AnswerDto, FormControl> = {
        text: new FormControl(null, Validators.required),
        isCorrect: new FormControl(),
        displayIndex: new FormControl(),
        correctIndex: new FormControl(),
      };
      answersFormArray.push(new FormGroup(answerForm));
    }
    this.form.setValue(question);
  }

  registerOnChange(onChange: (question?: QuestionForm) => void) {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: () => void) {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  validate(form: FormGroup): ValidationErrors | null {
    return form.errors;
  }

  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this.ngZone.onStable.pipe(take(1)).subscribe(() => this.autosize.resizeToFitContent(true));
  }
}
