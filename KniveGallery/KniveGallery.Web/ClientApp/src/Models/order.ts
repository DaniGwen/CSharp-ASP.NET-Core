export class Order {
  orderId: number;
  kniveId: number;
  kniveIds = new Array<number>();
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: number;
  address: string;
  street: string;
  neighbourhood: string;
  city: string;
  quantity: number;
  price: number;
  isDelivered: boolean;
  orderDate: string;
  dispatchDate: string;
}
