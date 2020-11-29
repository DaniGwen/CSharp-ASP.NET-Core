import { Component, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EventEmitter } from '@angular/core';
import { Knive } from '../../Models/knive';
import { Order } from '../../Models/order';
import { OrderService } from '../../Services/orders.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  @Input() public knive: Knive;
  public totalPrice: number;
  private order = new Order();
  public errorMessage: string;
  public showLoader: boolean;

  orderForm = this.formBuilder.group({
    firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', [Validators.required, Validators.min(4)]],
    city: ['', [Validators.required, Validators.minLength(3)]],
    neighbourhood: [''],
    street: ['', [Validators.required, Validators.minLength(3)]],
    quantity: [1],
    price: []
  });

  constructor(private formBuilder: FormBuilder,
    private orderService: OrderService,
    private router: Router) {
  }

  ngOnInit() {
    this.totalPrice = this.knive.price;
  }

  calculateKnivePrice(value: number) {
    this.totalPrice = this.knive.price * value;
  }

  OnSubmit() {
    this.showLoader = true;

    this.order = this.orderForm.value;
    this.order.price = this.totalPrice;
    this.order.kniveId = this.knive.kniveId;

    this.orderService.postOrder(this.order)
      .subscribe((order) => {
        if (order) {
          this.showLoader = false;
        }
        this.router.navigateByUrl("/order-summary");
      });


  }
}
