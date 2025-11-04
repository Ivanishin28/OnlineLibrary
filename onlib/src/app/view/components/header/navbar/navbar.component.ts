import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { switchMap, take } from 'rxjs';
import { Router } from '@angular/router';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { MenuModule } from 'primeng/menu';
import { AccountService } from '../../../../business/services/auth/account.service';
import { IdentityPreview } from '../../../../business/models/identity/identityPreview';
import { UserAvatarComponent } from '../../user/user-avatar/user-avatar.component';
import { CommonModule } from '@angular/common';
import { BookSearchComponent } from "../../books/book-search/book-search.component";
import { ProfileWindowManager } from '../../../../business/managers/windows/profileWindowManager';
import { BookPreview } from '../../../../business/models/books/bookPreview';

@Component({
  selector: 'navbar',
  imports: [
    MenubarModule,
    MenuModule,
    AvatarGroupModule,
    AvatarModule,
    UserAvatarComponent,
    CommonModule,
    BookSearchComponent
],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  public items: MenuItem[];
  public accountItems: MenuItem[];
  public identity: IdentityPreview | undefined;

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private router: Router,
    private profileWindowManager: ProfileWindowManager
  ) {
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
        label: 'Profile',
        command: () => this.profileWindowManager.open().subscribe(),
      },
      {
        label: 'LogOut',
        command: () => this.authService.logout(),
      },
    ];
  }

  public ngOnInit(): void {
    this.authService.loggedUser$
      .pipe(
        take(1),
        switchMap((creds) => this.accountService.getIdentityBy(creds.userId))
      )
      .subscribe((identity) => (this.identity = identity));
  }

  public onBookSelected(book: BookPreview): void {
    this.router.navigate(['books', book.id]);
  }
}
