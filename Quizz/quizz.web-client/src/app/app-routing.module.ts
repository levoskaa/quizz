import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SigninCallbackComponent } from './core/auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './core/auth/signout-callback/signout-callback.component';

const routes: Routes = [
  { path: 'signin-callback', component: SigninCallbackComponent },
  { path: 'signout-callback', component: SignoutCallbackComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
