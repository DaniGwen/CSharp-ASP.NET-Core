import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../../../Models/order';
import { OrderService } from '../../../Services/orders.service';

@Component({
  selector: 'app-order-manager',
  templateUrl: './order-manager.component.html',
  styleUrls: ['./order-manager.component.scss']
})

export class OrderManagerComponent implements OnInit {
  public orders: Order[];
  public errorMessage: string;
  public orderTitle: string = "Pending orders";
  public showLoader: boolean;

  constructor(private ordersService: OrderService,
    private router: Router) {
  }

  ngOnInit() {
    this.getAllOrders();
  }

  getAllOrders() {
    this.showLoader = true;
    this.ordersService.getOrders().subscribe((orders: Order[]) => {
      if (orders) {
        this.orders = orders;
        this.showLoader = false;
        this.orderTitle = "All orders"
      }
    });
  }

  deleteOrder(orderId: number) {
    this.showLoader = true;

    this.ordersService.deleteOrder(orderId).subscribe((data) => {
      this.ordersService.getOrders().subscribe((orders: Order[]) => {
        if (orders) {
          this.showLoader = false;
          this.orders = orders;
        }
        err => {
          this.errorMessage = "Could not delete order.";
        }
      });
    });
  }

  dispatchOrder(orderId: number) {
    this.ordersService.dispatchOrder(orderId).subscribe();
    this.getAllOrders();
    this.filterOrders("Send orders");
  }

  filterOrders(orderStatus: string) {
    this.showLoader = true;
    if (orderStatus === "Pending orders" || orderStatus === "Изчакващи поръчки") {
      this.ordersService.getOrders().subscribe((orders: Order[]) => {
        if (orders) {
          this.showLoader = false;
          this.orders = orders.filter(o => o.isDelivered === false);
        }
      });
      this.orderTitle = orderStatus;
    }
    else if (orderStatus === "Send orders" || orderStatus === "Изпратени поръчки") {
      this.ordersService.getOrders().subscribe((orders: Order[]) => {
        if (orders) {
          this.showLoader = false;
          this.orders = orders.filter(o => o.isDelivered === true);
        }
      });
      this.orderTitle = orderStatus;
    }
    else {
      this.getAllOrders();
    }
  }
}
