import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Knive } from '../../Models/knive';
import { KnivesService } from '../../Services/knives.service';

@Component({
  selector: 'app-knive-card',
  templateUrl: './knive-card.component.html',
  styleUrls: ['./knive-card.component.scss']
})

export class KniveCardComponent {

  public isAuthenticated: boolean;
  @Input() public knive: Knive;
  @Output() public deleteKniveRequest = new EventEmitter<number>();
  public message: string = '';
  private isLiked: boolean = false;
  public iconClass: string = "far fa-heart";

  constructor(private kniveService: KnivesService,
    private authService: AuthorizeService) {
  }

  ngOnInit() {
    this.authService.isAuthenticated().subscribe((auth: boolean) => {
      this.isAuthenticated = auth;
    });
  }

  deleteKnive(kniveId: number) {
    this.deleteKniveRequest.emit(kniveId);
  }

  addLike() {
    this.isLiked = !this.isLiked;

    if (this.isLiked) {
      this.iconClass = "fas fa-heart";
    }
    else {
      this.iconClass = "far fa-heart";
    }

    this.kniveService.updateKniveLikes(this.isLiked, this.knive.kniveId).subscribe((knive: Knive) => {
      this.knive = knive;
    });

    setInterval(() => { this.message = '' }, 5000);
  }
}
