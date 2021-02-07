import { Component, Input, Output, EventEmitter } from '@angular/core';
import {Router } from '@angular/router';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Knive } from '../../Models/knive';
import { KnivesService } from '../../Services/knives.service';
import { ShoppingCartService } from "../../Services/shopping-cart.service";


@Component({
  selector: 'app-knive-card',
  templateUrl: './knive-card.component.html',
  styleUrls: ['./knive-card.component.scss']
})

export class KniveCardComponent {
  public isAuthenticated: boolean;
  @Input() kniveClass: string;
  @Input() public knive: Knive;
  @Output() public deleteKniveRequest = new EventEmitter<number>();
  public message: string = '';
  private isLiked: boolean = false;
  public iconClass: string = "far fa-heart";
  public isAddedToCart: boolean = false;
  public isLoading: boolean = false;

  constructor(private kniveService: KnivesService,
    private authService: AuthorizeService,
    private cartService: ShoppingCartService,
    private router: Router) {
  }

  ngOnInit() {
    this.authService.isAuthenticated().subscribe((auth: boolean) => {
      this.isAuthenticated = auth;
      this.knive.quantityOrdered = 1;
    });

    var isKnifeAdded = this.cartService
      .getItems()
      .find((knive) => this.knive.kniveId === knive.kniveId);
    if (isKnifeAdded != null) {
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

  quantityOrdered(quantity: number) {
    this.knive.quantityOrdered = quantity;
  }

  checkQuantityValid(knive: Knive, quantityOrdered: number) {
    if (quantityOrdered > knive.quantity) {
      return;
    }
    else {
      knive.quantity -= quantityOrdered;
    }
  }

  removeFromCart() {
    this.cartService.removeFromCart(this.knive);
    this.isAddedToCart = false;
  }
}
