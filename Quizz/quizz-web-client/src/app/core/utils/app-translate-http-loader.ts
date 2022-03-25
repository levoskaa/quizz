import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { Observable, of } from 'rxjs';

export class AppTranslateHttpLoader extends TranslateHttpLoader {
  constructor(http: HttpClient) {
    super(http);
  }

  // eslint-disable-next-line @typescript-eslint/ban-types
  override getTranslation(lang: string): Observable<Object> {
    // We use modularized translations, so 'hu.json', 'en.json' etc.
    // shouldn't be loaded. (They don't exist.)
    if (['hu'].includes(lang)) {
      return of({});
    }
    return super.getTranslation(lang);
  }
}
