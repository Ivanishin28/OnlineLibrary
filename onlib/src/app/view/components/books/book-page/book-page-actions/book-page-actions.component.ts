import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { UserCredentials } from '../../../../../business/models/_shared/userCredentials';
import { AuthService } from '../../../../../business/services/auth/auth.service';
import { switchMap, take } from 'rxjs';
import { ShelvedBook } from '../../../../../business/models/shelves/shelvedBook';
import { ShelvedBookService } from '../../../../../business/services/shelves/shelvedBook.service';
import { CommonModule } from '@angular/common';
import { SelectModule } from 'primeng/select';
import { Shelf } from '../../../../../business/models/shelves/shelf';
import { ShelfService } from '../../../../../business/services/shelves/shelf.service';
import { FormsModule } from '@angular/forms';
import { TagSelectionComponent } from '../../../shelvesContext/tag-selection/tag-selection.component';

@Component({
  standalone: true,
  selector: 'book-page-actions',
  imports: [CommonModule, SelectModule, FormsModule, TagSelectionComponent],
  providers: [ShelvedBookService, ShelfService],
  templateUrl: './book-page-actions.component.html',
  styleUrl: './book-page-actions.component.scss',
})
export class BookPageActionsComponent implements OnInit, OnChanges {
  @Input({ required: true }) bookId!: string;

  public selectedShelf: Shelf | undefined;
  public shelves: Shelf[] = [];

  public shelvedBook: ShelvedBook | undefined;

  constructor(
    private authService: AuthService,
    private shelvedBookService: ShelvedBookService,
    private shelfService: ShelfService
  ) {}

  public ngOnInit(): void {
    this.authService.loggedUser$
      .pipe(
        take(1),
        switchMap((user) => this.shelfService.getByUserId(user.userId))
      )
      .subscribe((x) => {
        this.shelves = x;

        if (this.shelvedBook) {
          this.setShelvedBook(this.shelvedBook);
        }
      });
  }

  public ngOnChanges(): void {
    this.loadShelvedBook();
  }

  public shelveBookOn(shelf: Shelf): void {
    this.shelvedBookService
      .shelve(shelf.id, this.bookId)
      .subscribe(() => this.loadShelvedBook());
  }

  private loadShelvedBook(): void {
    this.shelvedBookService
      .get(this.bookId)
      .subscribe((x) => this.setShelvedBook(x));
  }

  private setShelvedBook(book: ShelvedBook | undefined) {
    this.shelvedBook = book;

    if (book) {
      this.selectedShelf = this.shelves.find(
        (shelf) => book?.shelf_id == shelf.id
      );
    } else {
      this.selectedShelf = undefined;
    }
  }

  public dislodge(): void {
    if (!this.shelvedBook) {
      return;
    }

    this.shelvedBookService
      .dislodge(this.shelvedBook.id)
      .subscribe((result) => {
        if (!result.isSuccess) {
          return;
        }

        this.shelvedBook = undefined;
      });
  }
}
