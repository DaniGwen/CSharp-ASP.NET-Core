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
  public knive: Knive;
  private kniveId: any;
  public totalPrice: number;
  private order = new Order();
  public message: boolean = false;
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
    quantity: [1],
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
    });
    this.kniveService.getKniveById(this.kniveId).subscribe((knive: Knive) => {
      this.knive = knive;
      this.showLoader = false;
    })
    this.totalPrice = this.knive.price;
  }

  calculateKnivePrice(value: number) {
    if (value > this.knive.quantity) {
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
    this.order.kniveId = this.knive.kniveId;

    this.knive.quantity -= this.quantityOrdered;
    this.orderService.postOrder(this.order).subscribe((message) => {
      if (message) {
        this.message = message;
      }
    });

    await this.delay(1000);

    this.kniveService.updateKnive(this.knive).subscribe(message => {
      if (message) {
        this.message = message;
        this.showLoader = false;
      }     
    });

    this.router.navigateByUrl("/order-summary");
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
