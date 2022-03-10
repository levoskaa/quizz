import { User } from 'oidc-client';

export class UserLoggedIn {
  static readonly type = '[Auth] User Logged In';
  constructor(public user: User) {}
}

export class UserLoggedout {
  static readonly type = '[Auth] User Logged Out';
}
