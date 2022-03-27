import { APP_INITIALIZER } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { combineLatest, Observable, of } from 'rxjs';
import { take } from 'rxjs/operators';
import { TranslateLoaderService } from '../services/translate-loader.service';

function translateFactory(
  translate: TranslateService,
  translateLoader: TranslateLoaderService
): () => Observable<void> {
  return () => {
    translate.setDefaultLang('hu');
    translate.use('hu');

    // eslint-disable-next-line @typescript-eslint/ban-types
    const translateObservables: Observable<Object>[] = [];
    for (const translation of ['common', 'shared']) {
      translateObservables.push(translateLoader.loadTranslation(translation));
    }
    combineLatest(translateObservables).pipe(take(1)).subscribe();

    return of();
  };
}

export const TRANSLATE_INITIALIZER = {
  provide: APP_INITIALIZER,
  useFactory: translateFactory,
  deps: [TranslateService, TranslateLoaderService],
  multi: true,
};
