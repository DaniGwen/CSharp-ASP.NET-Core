import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Knive } from 'src/Models/knive';
import { Order } from 'src/Models/order';
import { KnivesService } from 'src/Services/knives.service';
import { OrderService } from 'src/Services/orders.service';
import { ShoppingCartService } from 'src/Services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart-details',
  templateUrl: './shopping-cart-details.component.html',
  styleUrls: ['./shopping-cart-details.component.scss']
})
export class ShoppingCartDetailsComponent {
  orderForm = this.formBuilder.group({
    firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', [Validators.required, Validators.min(4)]],
    city: ['', [Validators.required, Validators.minLength(3)]],
    neighbourhood: [''],
    street: ['', [Validators.required, Validators.minLength(3)]],
    quantity: [1],
    price: [''],
  });

  showLoader: boolean = false;
  knives = new Array<Knive>();
  quantityOrdered: any;
  order = new Order();
  status: any;
  totalPrice: number;
  message: any;

  constructor(private cartService: ShoppingCartService,
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    private kniveService: KnivesService,
    private router: Router) {
  }

  ngOnInit() {
    this.knives = this.cartService.getItems();
    this.calculateTotalPrice();
  }

  async OnSubmit() {
    this.showLoader = true;
    this.order = this.orderForm.value;
    this.order.kniveIds = new Array<number>();
    this.knives.forEach(knive => {
      this.order.kniveIds.push(knive.kniveId);
      knive.quantity -= knive.quantityOrdered;
      this.updateKniveQuantity(knive);
    });
    this.order.price = this.totalPrice;

    this.orderService.postOrder(this.order).subscribe();
    this.cartService.clearCart();
    await this.delay(500);
    this.router.navigateByUrl("/order-summary");
  }

  updateKniveQuantity(knive: Knive) {
    this.kniveService.updateKnive(knive).subscribe(message => {
      if (message) {
        this.status = message;
        this.showLoader = false;
      }
    });
  }

  calculateTotalPrice(): number {
    this.totalPrice = this.knives.reduce((sum, knive) => sum + knive.price, 0);
    return this.totalPrice;
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
