import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { ShoppingCartService } from 'src/Services/shopping-cart.service';
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
  public isAddedToCart: boolean = false;
  public isLoading: boolean = false;

  constructor(private kniveService: KnivesService,
    private authService: AuthorizeService,
    private cartService: ShoppingCartService) {
  }

  ngOnInit() {
    this.authService.isAuthenticated().subscribe((auth: boolean) => {
      this.isAuthenticated = auth;

      //REMOVE just for testing
      this.addToCart();
    });

    var addedKnive = this.cartService.getItems().find((knive) => this.knive.kniveId == knive.kniveId)
    if (addedKnive != null) {
      this.isAddedToCart = true;
    }
  }

  deleteKnive(kniveId: number) {
    this.deleteKniveRequest.emit(kniveId);
  }

  addLike() {
    this.isLoading = true;
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

    this.isLoading = false;
  }

  addToCart() {
    this.cartService.addToCart(this.knive);
    this.isAddedToCart = true;
  }
}
