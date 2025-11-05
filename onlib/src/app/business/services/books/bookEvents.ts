import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class BookEvents {
  private _bookShelved = new Subject<string>();
  private _bookDislodged = new Subject<string>();
  private _bookReviewed = new Subject<number>();

  public readonly bookShelved$ = this._bookShelved.asObservable();
  public readonly bookDislodged$ = this._bookDislodged.asObservable();
  public readonly bookReviewed$ = this._bookReviewed.asObservable();

  public shelveBook(bookId: string): void {
    this._bookShelved.next(bookId);
  }

  public dislodgeBook(bookId: string): void {
    this._bookDislodged.next(bookId);
  }

  public reviewBook(rating: number): void {
    this._bookReviewed.next(rating);
  }
}
