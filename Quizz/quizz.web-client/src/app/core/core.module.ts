import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SigninCallbackComponent } from './auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './auth/signout-callback/signout-callback.component';
import { TokenInterceptor } from './interceptors/token.interceptor';

@NgModule({
  declarations: [SigninCallbackComponent, SignoutCallbackComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }],
  imports: [CommonModule],
})
export class CoreModule {}
