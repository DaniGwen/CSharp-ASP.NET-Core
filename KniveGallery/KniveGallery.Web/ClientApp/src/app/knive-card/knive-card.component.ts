import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Knive } from '../../Models/knive';

@Component({
  selector: 'app-knive-card',
  templateUrl: './knive-card.component.html',
  styleUrls: ['./knive-card.component.scss']
})

export class KniveCardComponent {

  @Input() public knive: Knive;
  @Input() public isAuthenticated: boolean;
  @Output() public deleteKniveRequest = new EventEmitter<number>();

  constructor() {
  }

  deleteKnive(kniveId: number) {
    this.deleteKniveRequest.emit(kniveId);
  }
}
