import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { take } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'navbar',
  imports: [MenubarModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  public items: MenuItem[];

  constructor(private authService: AuthService, private router: Router) {
    this.items = [
      {
        label: 'Books',
        routerLink: 'books',
      },
      {
        label: 'Register',
        routerLink: 'account/register',
      },
      {
        label: 'Login',
        routerLink: 'account/login',
      },
      {
        label: 'Organization',
        routerLink: 'organization',
      },
      {
        label: 'Library',
        command: () =>
          this.authService.loggedUser$
            .pipe(take(1))
            .subscribe((x) =>
              this.router.navigate(['library', x.userId.value])
            ),
      },
      {
        label: 'LogOut',
        command: () => this.authService.logout(),
      },
    ];
  }
}
