import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { SigninCallbackComponent } from './auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './auth/signout-callback/signout-callback.component';
import { TRANSLATE_INITIALIZER } from './initializers/translate.initializer';
import { TokenInterceptor } from './interceptors/token.interceptor';

const appInitializers = [TRANSLATE_INITIALIZER];

function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [SigninCallbackComponent, SignoutCallbackComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }, ...appInitializers],
  imports: [
    CommonModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
  exports: [BrowserModule, HttpClientModule, BrowserAnimationsModule],
})
export class CoreModule {}
