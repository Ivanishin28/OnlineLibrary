import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { SystemConsts } from '../../business/consts/_shared/systemConsts';

@Pipe({
  pure: true,
  name: 'onlibDate',
})
export class OnlibDatePipe implements PipeTransform {
  constructor(private datePipe: DatePipe) {}

  public transform(value: Date): string {
    if (!value) {
      return '';
    }

    return this.datePipe.transform(value, SystemConsts.DATE_FORMAT) ?? '';
  }
}
