import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthState } from '../states/auth.state';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private readonly store: Store) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.startsWith(Globals.apiRoot)) {
      return this.store.select(AuthState.accessToken).pipe(
        switchMap((token) => {
          const headers = request.headers.set('Authorization', `Bearer ${token}`);
          const authenticatedRequest = request.clone({ headers });
          return next.handle(authenticatedRequest);
        })
      );
    } else {
      return next.handle(request);
    }
  }
}
