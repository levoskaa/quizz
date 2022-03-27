import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator,
  Validators,
} from '@angular/forms';
import { tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/shared/classes/unsubscribe-on-destroy';
import { UpdateGameDto } from 'src/app/shared/models/generated/game-generated.models';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: GameFormComponent,
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: GameFormComponent,
      multi: true,
    },
  ],
  encapsulation: ViewEncapsulation.None,
  host: {
    class: 'app-game-form',
  },
})
export class GameFormComponent
  extends UnsubscribeOnDestroy
  implements OnInit, ControlValueAccessor, Validator
{
  formControls: Record<keyof UpdateGameDto, FormControl> = {
    name: new FormControl(null, Validators.required),
  };
  form = new FormGroup(this.formControls);

  // eslint-disable-next-line
  onChange = (value: UpdateGameDto) => {};
  // eslint-disable-next-line
  onTouched = () => {};

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.subscribe(
      this.form.valueChanges.pipe(
        tap((value) => {
          this.onTouched();
          this.onChange(value);
        })
      )
    );
  }

  writeValue(value: UpdateGameDto): void {
    this.form.patchValue(value, { emitEvent: false });
  }

  registerOnChange(fn: (value: UpdateGameDto) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(disabled: boolean): void {
    if (disabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  validate(_: AbstractControl): ValidationErrors | null {
    return this.form.valid ? null : { game: 'invalid' };
  }
}
