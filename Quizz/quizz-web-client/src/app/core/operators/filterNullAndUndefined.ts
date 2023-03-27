import { OperatorFunction } from 'rxjs';
import { filter } from 'rxjs/operators';

export function filterNullAndUndefined<InType>(): OperatorFunction<InType, NonNullable<InType>> {
  return filter(
    (value: InType): value is NonNullable<InType> => value !== null && value !== undefined
  );
}
