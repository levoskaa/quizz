import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store';
import { User } from 'oidc-client';
import { UserLoggedIn, UserLoggedout } from '../actions/auth.actions';

const AUTH_STATE_TOKEN = new StateToken<AuthStateModel>('auth');

interface AuthStateModel {
  user: User | undefined;
}

@State<AuthStateModel>({
  name: AUTH_STATE_TOKEN,
  defaults: {
    user: undefined,
  },
})
@Injectable()
export class AuthState {
  @Selector()
  static user(state: AuthStateModel): User | undefined {
    return state.user;
  }

  @Selector()
  static isAuthenticated({ user }: AuthStateModel): boolean {
    const isAuthenticated = !!user && !user.expired;
    return isAuthenticated;
  }

  @Selector()
  static accessToken({ user }: AuthStateModel): string | undefined {
    return user?.access_token;
  }

  @Action(UserLoggedIn)
  userLoggedIn(ctx: StateContext<AuthStateModel>, action: UserLoggedIn) {
    ctx.patchState({
      user: action.user,
    });
  }

  @Action(UserLoggedout)
  userLoggedOut(ctx: StateContext<AuthStateModel>) {
    ctx.patchState({
      user: undefined,
    });
  }
}
