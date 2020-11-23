import { Component, OnInit } from '@angular/core';
import { Order } from '../../Models/order';
import { OrderService } from '../../Services/orders.service';

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

  constructor(private ordersService: OrderService) {
    this.ordersService.getOrders().subscribe((orders: Order[]) => {
      this.orders = orders;
    });
  }

  ngOnInit() {
  }

  deleteOrder(orderId: number) {
    this.showLoader = true;

    this.ordersService.deleteOrder(orderId).subscribe((data) => {
      this.ordersService.getOrders().subscribe((orders: Order[]) => {
        if (orders) {
          this.showLoader = false;
        }
        this.orders = orders;
      });
    });
  }

  dispatchOrder(orderId: number) {
    this.ordersService.dispatchOrder(orderId).subscribe();
    this.ngOnInit();
  }

  filterOrders(orderStatus: string) {
    this.showLoader = true;
    if (orderStatus === "Pending orders" || orderStatus === "Изчакващи поръчки") {
      this.ordersService.getOrders().subscribe((orders: Order[]) => {
        if (orders) {
          this.showLoader = false;
        }
        this.orders = orders.filter(o => o.isDelivered === false);
      });
      this.orderTitle = orderStatus;
    }
    else {
      this.ordersService.getOrders().subscribe((orders: Order[]) => {
        if (orders) {
          this.showLoader = false;
        }
        this.orders = orders.filter(o => o.isDelivered === true);
      });
      this.orderTitle = orderStatus;
    }

    this.ngOnInit();
  }
}
