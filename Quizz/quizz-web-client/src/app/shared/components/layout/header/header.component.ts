import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';
import { AuthState } from 'src/app/core/states/auth.state';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  isAuthenticated$: Observable<boolean>;

  constructor(private readonly auth: AuthService, private readonly store: Store) {}

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.select(AuthState.isAuthenticated);
  }

  login(): void {
    this.auth.startAuthentication();
  }

  logout(): void {
    this.auth.startLogout();
  }
}
