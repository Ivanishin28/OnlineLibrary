import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { take } from 'rxjs';
import { Router } from '@angular/router';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { MenuModule } from 'primeng/menu';

@Component({
  selector: 'navbar',
  imports: [MenubarModule, MenuModule, AvatarGroupModule, AvatarModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  public items: MenuItem[];
  public accountItems: MenuItem[];

  constructor(private authService: AuthService, private router: Router) {
    this.items = [
      {
        label: 'Books',
        routerLink: 'books',
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
    ];

    this.accountItems = [
      {
        label: 'LogOut',
        command: () => this.authService.logout(),
      },
    ];
  }
}
