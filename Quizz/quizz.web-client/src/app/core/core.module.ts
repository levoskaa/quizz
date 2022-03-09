import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SigninCallbackComponent } from './auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './auth/signout-callback/signout-callback.component';
import { TokenInterceptor } from './interceptors/token.interceptor';

@NgModule({
  declarations: [SigninCallbackComponent, SignoutCallbackComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }],
  imports: [CommonModule],
  exports: [BrowserModule, HttpClientModule, BrowserAnimationsModule],
})
export class CoreModule {}
