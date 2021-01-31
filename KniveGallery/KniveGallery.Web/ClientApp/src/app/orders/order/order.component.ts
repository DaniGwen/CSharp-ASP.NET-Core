import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Knive } from '../../../Models/knive';
import { Order } from '../../../Models/order';
import { KnivesService } from '../../../Services/knives.service';
import { OrderService } from '../../../Services/orders.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  public knive = new Knive();
  private kniveId: any;
  public kniveClass: string;
  public totalPrice: number;
  private order = new Order();
  public message: boolean = false;
  public status: any;
  public showLoader: boolean;
  private quantityOrdered: number;

  orderForm = this.formBuilder.group({
    firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', [Validators.required, Validators.min(4)]],
    city: ['', [Validators.required, Validators.minLength(3)]],
    neighbourhood: [''],
    street: ['', [Validators.required, Validators.minLength(3)]],
    quantity: [''],
    price: ['']
  });

  constructor(private formBuilder: FormBuilder,
    private orderService: OrderService,
    private kniveService: KnivesService,
    private router: Router,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.showLoader = true;
    this.route.paramMap.subscribe(params => {
      this.kniveId = params.get('id');
      this.kniveClass = params.get('kniveClass');
      this.kniveService.getKniveById(this.kniveId).subscribe((data: any) => {
        this.knive = data;
        this.showLoader = false;
      })
    });

    this.totalPrice = this.knive.price;

    this.calculateKnivePrice(1);
  }

  calculateKnivePrice(value: number) {
    if (value > this.knive.quantity) {
      this.totalPrice = 0;
      return;
    }
    this.quantityOrdered = value;
    this.totalPrice = this.knive.price * value;
  }

  async OnSubmit() {
    if (this.quantityOrdered > this.knive.quantity) {
      return;
    }
    this.showLoader = true;
    this.order = this.orderForm.value;
    this.order.price = this.totalPrice;
    this.order.kniveIds = new Array<number>();
    this.order.kniveIds.push(this.knive.kniveId);
    this.knive.quantity -= this.quantityOrdered;

    this.orderService.postOrder(this.order).subscribe((message) => {
      if (message) {
        this.message = message;
      }
    });

    await this.delay(500);

    this.kniveService.updateKnive(this.knive).subscribe(message => {
      if (message) {
        this.status = message;
        this.showLoader = false;
      }
    });

    this.router.navigateByUrl("/order-summary");
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
