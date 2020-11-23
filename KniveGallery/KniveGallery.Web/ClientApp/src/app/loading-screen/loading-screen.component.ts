import { Component, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-loading-screen',
  templateUrl: './loading-screen.component.html',
  styleUrls: ['./loading-screen.component.scss']
})
export class LoadingScreenComponent {

  @Input() public showOverlay: boolean;

  constructor() {
  }
}
