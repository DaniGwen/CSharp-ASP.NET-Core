import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCarouselImagesComponent } from './add-carousel-images/add-carousel-images.component';
import { AddKniveComponent } from './add-knive/add-knive.component';
import { EditKniveComponent } from './edit-knive/edit-knive.component';
import { HomeComponent } from './home/home.component';
import { KniveDetailsComponent } from './knive-details/knive-details.component';
import { KnivesComponent } from './knives/knives.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { OrderManagerComponent } from './orders/order-manager/order-manager.component';
import { OrderSummaryComponent } from './orders/order-summary/order-summary.component';
import { OrderComponent } from './orders/order/order.component';
import { PrivacyComponent } from './privacy/privacy.component';
import { ShoppingCartDetailsComponent } from './shopping-cart-details/shopping-cart-details.component';
import { ContactPageComponent } from './contact-page/contact-page.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { AuthorizeGuard } from "../api-authorization/authorize.guard";

const routes: Routes = [
  {
    path: "about-page",
    component: AboutPageComponent
  },
  {
    path: "contact-page",
    component: ContactPageComponent
  },
  {
    path: "knives/:kniveClass",
    component: KnivesComponent
  },
  {
    path: "shopping-cart-details",
    component: ShoppingCartDetailsComponent
  },
  {
    path: "add-carousel-images",
    component: AddCarouselImagesComponent,
    canActivate: [AuthorizeGuard]
  },
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'knive-details/:id',
    component: KniveDetailsComponent
  },

  {
    path: 'privacy',
    component: PrivacyComponent
  },
  {
    path: 'order/:id',
    component: OrderComponent
  },
  {
    path: 'order-summary',
    component: OrderSummaryComponent
  },
  {
    path: 'order-manager',
    component: OrderManagerComponent,
    canActivate: [AuthorizeGuard]
  },
  {
    path: 'edit-knive/:id',
    component: EditKniveComponent,
    canActivate: [AuthorizeGuard]
  },
  {
    path: 'add-knive',
    component: AddKniveComponent,
    canActivate: [AuthorizeGuard]
  },
  {
    path: '**',
    component: NotFoundComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
