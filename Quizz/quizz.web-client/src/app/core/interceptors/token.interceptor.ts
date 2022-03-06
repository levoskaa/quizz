import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { from, Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private readonly authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.startsWith(Globals.apiRoot)) {
      return from(
        this.authService.getAccessToken().then((token) => {
          const headers = request.headers.set('Authorization', `Bearer ${token}`);
          const authRequest = request.clone({ headers });
          return next.handle(authRequest).toPromise();
        })
      );
    } else {
      return next.handle(request);
    }
  }
}
