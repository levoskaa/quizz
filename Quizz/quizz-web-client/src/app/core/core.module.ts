import { CommonModule, DatePipe } from '@angular/common';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { environment } from 'src/environments/environment';
import { SigninCallbackComponent } from './auth/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './auth/signout-callback/signout-callback.component';
import { TRANSLATE_INITIALIZER } from './initializers/translate.initializer';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { AuthState } from './states/auth.state';
import { AppTranslateHttpLoader } from './utils/app-translate-http-loader';
import { LoaderInterceptor } from './interceptors/loader.interceptor';

const appInitializers = [TRANSLATE_INITIALIZER];

function HttpLoaderFactory(http: HttpClient) {
  return new AppTranslateHttpLoader(http);
}

@NgModule({
  declarations: [SigninCallbackComponent, SignoutCallbackComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    DatePipe,
    ...appInitializers,
  ],
  imports: [
    CommonModule,
    TranslateModule.forRoot({
      defaultLanguage: 'hu',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    NgxsModule.forRoot([AuthState], {
      developmentMode: !environment.production,
    }),
    NgxsLoggerPluginModule.forRoot({ disabled: environment.production }),
    NgxsStoragePluginModule.forRoot({ key: [AuthState] }),
    NgxsReduxDevtoolsPluginModule.forRoot({ disabled: environment.production }),
  ],
  exports: [BrowserModule, HttpClientModule, BrowserAnimationsModule],
})
export class CoreModule {}
