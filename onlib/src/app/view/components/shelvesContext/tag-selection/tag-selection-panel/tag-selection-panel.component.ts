import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TagSelection } from '../../../../../business/models/shelves/tagSelection';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'tag-selection-panel',
  imports: [CommonModule, CheckboxModule, FormsModule],
  templateUrl: './tag-selection-panel.component.html',
  styleUrl: './tag-selection-panel.component.scss',
})
export class TagSelectionPanelComponent {
  @Input({ required: true }) tags!: TagSelection[];
}
