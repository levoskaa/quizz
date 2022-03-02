import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { SignoutResponse, User, UserManager, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userManager: UserManager;
  private userSubject = new BehaviorSubject<User | undefined>(undefined);
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  get isAuthenticated$(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  get user$(): Observable<User | undefined> {
    return this.userSubject.asObservable();
  }

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

  constructor() {
    this.userManager = new UserManager(this.idpSettings);
  }

  startAuthentication(): void {
    this.userManager.signinRedirect();
  }

  async completeAuthentication(): Promise<void> {
    const user = await this.userManager.signinRedirectCallback();
    this.userSubject.next(user);
    this.isAuthenticatedSubject.next(this.checkUser(user));
  }

  startLogout(): void {
    this.userManager.signoutRedirect();
  }

  completeLogout(): Promise<SignoutResponse> {
    this.userSubject.next(undefined);
    return this.userManager.signoutRedirectCallback();
  }

  async isAuthenticated(): Promise<boolean> {
    const user = (await this.userManager.getUser()) ?? undefined;
    if (this.userSubject.value !== user) {
      this.userSubject.next(user);
    }
    return this.checkUser(user);
  }

  async getAccessToken(): Promise<string> {
    const user = await this.userManager.getUser();
    return !!user && !user.expired ? user.access_token : '';
  }

  private checkUser(user: User | undefined): boolean {
    return !!user && !user.expired;
  }
}
