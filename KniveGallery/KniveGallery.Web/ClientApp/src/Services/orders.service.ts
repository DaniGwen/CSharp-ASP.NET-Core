import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment'
import { Order } from '../Models/order';

@Injectable()
export class OrderService {
  private headers = new HttpHeaders();
  Url = environment.apiUrl + "/api/orders";

  constructor(private http: HttpClient) {
  }

  public postOrder(order: Order): Observable<any> {
    return this.http.post(`${this.Url}/AddOrder`, order, { headers: this.headers });
  }

  public getOrders() {
    return this.http.get<Order[]>(this.Url, { headers: this.headers });
  }

  public deleteOrder(orderId: number) {
    return this.http.delete(`${this.Url}/${orderId}`, { headers: this.headers });
  }

  public dispatchOrder(orderId: number) {
    return this.http.post(`${this.Url}/DispatchOrder/${orderId}`, { headers: this.headers });
  }
}
