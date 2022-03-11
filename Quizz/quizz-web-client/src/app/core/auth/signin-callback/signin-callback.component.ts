import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signin-callback',
  template: '',
})
export class SigninCallbackComponent implements OnInit {
  constructor(private readonly authService: AuthService, private readonly router: Router) {}

  ngOnInit(): void {
    this.authService.completeAuthentication().then(() => {
      this.router.navigate(['/'], { replaceUrl: true });
    });
  }
}
