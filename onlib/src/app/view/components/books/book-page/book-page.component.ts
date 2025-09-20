import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { BookPreviewComponent } from '../../booksPage/book-preview/book-preview.component';

@Component({
  standalone: true,
  selector: 'book-page',
  imports: [CommonModule, RouterModule, BookPreviewComponent],
  providers: [BookService],
  templateUrl: './book-page.component.html',
  styleUrl: './book-page.component.scss',
})
export class BookPageComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public book: BookPreview | undefined;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService
  ) {}

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(
        takeUntil(this.destroy$),
        switchMap((params) => {
          const bookId = params.get('id')!;
          return this.bookService.getFull(bookId);
        })
      )
      .subscribe((book) => {
        this.book = book;
        console.log(book);
      });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
  }
}
