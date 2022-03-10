import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signout-callback',
  template: '',
})
export class SignoutCallbackComponent implements OnInit {
  constructor(private readonly authService: AuthService, private readonly router: Router) {}

  ngOnInit(): void {
    this.authService.completeLogout().then(() => {
      this.router.navigate(['/'], { replaceUrl: true });
    });
  }
}
