import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { switchMap, take, tap } from 'rxjs';
import { LibraryFilter } from '../../../../business/models/shelves/libraryFilter';
import { LibrarySummary } from '../../../../business/models/shelves/librarySummary';
import { ShelvedBookService } from '../../../../business/services/shelves/shelvedBook.service';
import { UserId } from '../../../../business/models/_shared/userId';
import { LibraryService } from '../../../../business/services/shelves/library.service';
import { LibraryFilterComponent } from "./library-filter/library-filter.component";

@Component({
  standalone: true,
  selector: 'library',
  imports: [CommonModule, RouterModule, LibraryFilterComponent],
  providers: [LibraryService],
  templateUrl: './library.component.html',
  styleUrl: './library.component.scss',
})
export class LibraryComponent implements OnInit {
  private userId: UserId | undefined;

  public librarySummary: LibrarySummary | undefined;

  constructor(
    private route: ActivatedRoute,
    private libraryService: LibraryService
  ) {}

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(
        take(1),
        tap((x) => (this.userId = new UserId(x.get('userId')!))),
        switchMap(() => this.libraryService.getLibrarySummaryBy(this.userId!))
      )
      .subscribe((x) => {
        this.librarySummary = x;
      });
  }
}
