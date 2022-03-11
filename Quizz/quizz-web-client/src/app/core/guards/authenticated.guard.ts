import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
  UrlTree,
} from '@angular/router';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { AuthState } from '../states/auth.state';

@Injectable({
  providedIn: 'root',
})
export class AuthenticatedGuard implements CanActivate, CanLoad {
  constructor(private readonly store: Store, private readonly router: Router) {}

  canActivate(
    _route: ActivatedRouteSnapshot,
    _state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.checkUserAuthentication();
    return true;
  }

  canLoad(
    _route: Route,
    _segments: UrlSegment[]
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.checkUserAuthentication();
    return true;
  }

  private checkUserAuthentication(): void {
    const isAuthenticated = this.store.selectSnapshot(AuthState.isAuthenticated);
    if (!isAuthenticated) {
      this.router.navigateByUrl('/');
    }
  }
}
