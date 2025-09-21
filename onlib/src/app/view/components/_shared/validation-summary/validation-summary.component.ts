import { Component, Input } from '@angular/core';
import { BusinessError } from '../../../../business/models/_shared/businessError';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-validation-summary',
  imports: [CommonModule],
  templateUrl: './validation-summary.component.html',
  styleUrl: './validation-summary.component.scss',
})
export class ValidationSummaryComponent {
  @Input({ required: true }) errors: BusinessError[] | undefined;
}
