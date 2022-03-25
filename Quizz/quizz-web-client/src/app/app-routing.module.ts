import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SigninCallbackComponent } from './core/auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './core/auth/signout-callback/signout-callback.component';
import { AuthenticatedGuard } from './core/guards/authenticated.guard';
import { TranslateResolver } from './core/utils/translate.resolver';
import { LayoutContainerComponent } from './shared/components/layout/layout-container/layout-container.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutContainerComponent,
    children: [
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
