import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MessageWindowInput } from '../../../../business/models/_shared/messageWindowInput';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';

@Component({
  standalone: true,
  selector: 'message-window',
  imports: [CommonModule],
  templateUrl: './message-window.component.html',
  styleUrl: './message-window.component.scss',
})
export class MessageWindowComponent {
  public input: MessageWindowInput;

  constructor(config: DynamicDialogConfig) {
    this.input = config.data;
  }
}
