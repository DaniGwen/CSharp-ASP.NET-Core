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
        quantity: [''],
        price: ['']
    });

    showLoader: boolean = false;
    knives: Array<Knive> = [];
    quantityOrdered: any;
    private order = new Order();
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
        if (this.knives != null) {
            this.knives.forEach(knive => {
                this.order.kniveIds.push(knive.kniveId);
            });
        }
    }

    async OnSubmit() {
        this.showLoader = true;
        this.order = this.orderForm.value;
        this.order.price = this.totalPrice;

        this.orderService.postOrder(this.order).subscribe((message) => {
            if (message) {
                this.message = message;
            }
        });

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

    checkQuantityValid(knive: Knive, quantityOrdered: number) {
        if (quantityOrdered > knive.quantity) {
            return;
        }
        else {
            knive.quantity -= quantityOrdered;
        }
    }

    calculateTotalPrice() {
        
    }

    delay(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}