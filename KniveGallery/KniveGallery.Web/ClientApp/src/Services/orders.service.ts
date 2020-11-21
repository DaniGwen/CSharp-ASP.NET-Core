import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment'
import { Order } from '../Models/order';

@Injectable()
export class OrderService {
  private headers = new HttpHeaders();
  Url = environment.apiUrl + "/api/orders";

  constructor(private http: HttpClient) {
  }

  public postOrder(order: Order) {
    return this.http.post<Order>(`${this.Url}/AddOrder`, order, { headers: this.headers });
  }
}
