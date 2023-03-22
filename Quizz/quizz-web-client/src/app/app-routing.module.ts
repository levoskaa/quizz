import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SigninCallbackComponent } from './core/auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './core/auth/signout-callback/signout-callback.component';
import { AuthenticatedGuard } from './core/guards/authenticated.guard';
import { TranslateResolver } from './core/utils/translate.resolver';
import { GameLobbyPageComponent } from './features/quiz-runner/pages/game-lobby-page/game-lobby-page.component';
import { JoinGamePageComponent } from './features/quiz-runner/pages/join-game-page/join-game-page.component';
import { LayoutContainerComponent } from './shared/components/layout/layout-container/layout-container.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutContainerComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: JoinGamePageComponent,
      },
      {
        path: 'runner',
        children: [
          {
            path: ':id',
            component: GameLobbyPageComponent,
          },
        ],
      },
      {
        path: 'games',
        loadChildren: () => import('./features/game/game.module').then((m) => m.GameModule),
        canActivate: [AuthenticatedGuard],
        canLoad: [AuthenticatedGuard],
        resolve: [TranslateResolver],
        data: {
          translation: 'game',
        },
      },
    ],
  },
  { path: 'signin-callback', component: SigninCallbackComponent },
  { path: 'signout-callback', component: SignoutCallbackComponent },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
