import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Knive } from '../Models/knive';

@Injectable({
    providedIn: 'root'
})
export class ShoppingCartService {
    private items: Array<Knive> = [];
    $itemsChange = new Subject<Knive[]>();

    addToCart(knive: Knive) {
        this.items.push(knive);
        this.$itemsChange.next(this.items);
    }

    getItems() {
        return this.items;
    }

    clearCart() {
        this.items = [];
        return this.$itemsChange.next(this.items);
    }
}
