import { APP_INITIALIZER } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';

function translateFactory(translate: TranslateService): () => Observable<void> {
  return () => {
    translate.setDefaultLang('hu');
    translate.use('hu');
    return of();
  };
}

export const TRANSLATE_INITIALIZER = {
  provide: APP_INITIALIZER,
  useFactory: translateFactory,
  deps: [TranslateService],
  multi: true,
};
