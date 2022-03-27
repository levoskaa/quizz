import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TranslateLoaderService } from '../services/translate-loader.service';

@Injectable({
  providedIn: 'root',
})
export class TranslateResolver implements Resolve<boolean> {
  constructor(private readonly translateLoader: TranslateLoaderService) {}

  resolve(route: ActivatedRouteSnapshot, _state: RouterStateSnapshot): Observable<boolean> {
    return this.translateLoader.loadTranslation(route.data.translation).pipe(map(() => true));
  }
}
