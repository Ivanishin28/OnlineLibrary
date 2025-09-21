import { Component, Input, OnChanges } from '@angular/core';
import { UserCredentials } from '../../../../../business/models/_shared/userCredentials';
import { AuthService } from '../../../../../business/services/auth/auth.service';
import { switchMap, take } from 'rxjs';
import { ShelvedBook } from '../../../../../business/models/shelves/shelvedBook';
import { ShelvedBookService } from '../../../../../business/services/shelves/shelvedBook.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'book-page-actions',
  imports: [CommonModule],
  providers: [ShelvedBookService],
  templateUrl: './book-page-actions.component.html',
  styleUrl: './book-page-actions.component.scss',
})
export class BookPageActionsComponent implements OnChanges {
  @Input({ required: true }) bookId!: string;

  private user!: UserCredentials;

  public shelvedBook: ShelvedBook | undefined;

  constructor(
    private authService: AuthService,
    private shelvedBookService: ShelvedBookService
  ) {}

  public ngOnChanges(): void {
    this.authService.loggedUser$
      .pipe(
        take(1),
        switchMap(() =>
          this.shelvedBookService.get(this.user.userId, this.bookId)
        )
      )
      .subscribe((x) => (this.shelvedBook = x));
  }
}
