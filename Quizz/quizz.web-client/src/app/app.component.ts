import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthService } from './core/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'quizz-web-client';

  constructor(private readonly authService: AuthService, private readonly httpClient: HttpClient) {}

  login(): void {
    this.authService.startAuthentication();
  }

  logout(): void {
    this.authService.startLogout();
  }

  createGame(): void {
    this.httpClient.post('https://localhost:32001/api/game/games', {}).subscribe();
  }
}
