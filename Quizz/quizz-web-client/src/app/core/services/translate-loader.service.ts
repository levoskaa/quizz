import { Injectable } from '@angular/core';
import { TranslateLoader, TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TranslateLoaderService {
  private loadedModules = new Set<string>();

  constructor(
    private readonly translateLoader: TranslateLoader,
    private readonly translateService: TranslateService
  ) {}

  // eslint-disable-next-line @typescript-eslint/ban-types
  loadTranslation(moduleName: string): Observable<Object> {
    if (this.loadedModules.has(moduleName)) {
      return of({});
    }
    return this.translateLoader
      .getTranslation(`${this.translateService.currentLang}/${moduleName}`)
      .pipe(
        tap((translation) => {
          this.translateService.setTranslation(
            this.translateService.currentLang,
            translation,
            true
          );
          this.loadedModules.add(moduleName);
        })
      );
  }
}
