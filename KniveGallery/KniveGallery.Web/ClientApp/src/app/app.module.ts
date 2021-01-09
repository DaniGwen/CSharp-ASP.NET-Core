import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { TranslateLoader, TranslateModule, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './core/nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { KniveDetailsComponent } from './knive-details/knive-details.component';
import { EditKniveComponent } from './edit-knive/edit-knive.component';
import { KnivesService } from '../Services/knives.service';
import { FooterComponent } from './core/footer/footer.component';
import { PrivacyComponent } from './privacy/privacy.component';
import { ContactInfoComponent } from "./contact-info/contact-info.component";
import { KniveCardComponent } from './knive-card/knive-card.component';
import { OrderComponent } from './orders/order/order.component';
import { OrderService } from '../Services/orders.service';
import { OrderSummaryComponent } from './orders/order-summary/order-summary.component'
import { OrderManagerComponent } from './orders/order-manager/order-manager.component';
import { LoadingScreenComponent } from './loading-screen/loading-screen.component';
import { NgcCookieConsentConfig, NgcCookieConsentModule } from 'ngx-cookieconsent';
import { environment } from '../environments/environment';
import { AddKniveComponent } from './add-knive/add-knive.component'
import { CoreModule } from './core/core.module';
import { AppRoutingModule } from './app.routing.module';
import { NotFoundComponent } from './not-found/not-found.component';
import { AsideCarouselComponent } from './aside-carousel/aside-carousel.component';
import { ImageService } from 'src/Services/images.service';
import { AddCarouselImagesComponent } from './add-carousel-images/add-carousel-images.component';
import { ShoppingCartService } from 'src/Services/shopping-cart.service';
import { ShoppingCartDetailsComponent } from './shopping-cart-details/shopping-cart-details.component';

const cookieConfig: NgcCookieConsentConfig = {
  cookie: {
    domain: environment.apiUrl
  },
  "position": "bottom-right",
  "theme": "classic",
  "palette": {
    "popup": {
      "background": "#000000",
      "text": "#ffffff",
      "link": "#ffffff"
    },
    "button": {
      "background": "#f1d600",
      "text": "#000000",
      "border": "transparent"
    }
  },
  "type": "info",
  "content": {
    "message": "This website uses cookies to ensure you get the best experience on our website.",
    "dismiss": "Got it!",
    "deny": "Refuse cookies",
    "link": "Learn more",
    "href": "https://cookiesandyou.com",
    "policy": "Cookie Policy"
  }
};

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    KniveDetailsComponent,
    EditKniveComponent,
    PrivacyComponent,
    ContactInfoComponent,
    KniveCardComponent,
    OrderComponent,
    OrderSummaryComponent,
    OrderManagerComponent,
    LoadingScreenComponent,
    AddKniveComponent,
    NotFoundComponent,
    AsideCarouselComponent,
    AddCarouselImagesComponent,
    ShoppingCartDetailsComponent
  ],
  imports: [
    CoreModule,
    HttpClientModule,
    NgcCookieConsentModule.forRoot(cookieConfig),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [
    { provide: ImageService, useClass: ImageService },
    { provide: AuthorizeGuard, useClass: AuthorizeGuard },
    { provide: TranslateService, useClass: TranslateService },
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: KnivesService, useClass: KnivesService },
    { provide: OrderService, useClass: OrderService },
    { provide: ShoppingCartService, useClass: ShoppingCartService }
  ],
  bootstrap: [
    AppComponent,
    NavMenuComponent,
    FooterComponent
  ]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
