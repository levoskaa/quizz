import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { Store } from '@ngxs/store';
import { SignoutResponse, UserManager, UserManagerSettings } from 'oidc-client';
import { UserLoggedIn, UserLoggedout } from '../actions/auth.actions';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userManager: UserManager;

  private get idpSettings(): UserManagerSettings {
    return {
      authority: Globals.idpAuthority,
      client_id: Globals.clientId,
      redirect_uri: `${Globals.clientRoot}/signin-callback`,
      scope: 'openid profile game',
      response_type: 'code',
      post_logout_redirect_uri: `${Globals.clientRoot}/signout-callback`,
    };
  }

  constructor(private readonly store: Store) {
    this.userManager = new UserManager(this.idpSettings);
  }

  startAuthentication(): void {
    this.userManager.signinRedirect();
  }

  async completeAuthentication(): Promise<void> {
    const user = await this.userManager.signinRedirectCallback();
    this.store.dispatch(new UserLoggedIn(user));
  }

  startLogout(): void {
    this.userManager.signoutRedirect();
  }

  completeLogout(): Promise<SignoutResponse> {
    this.store.dispatch(new UserLoggedout());
    return this.userManager.signoutRedirectCallback();
  }
}
