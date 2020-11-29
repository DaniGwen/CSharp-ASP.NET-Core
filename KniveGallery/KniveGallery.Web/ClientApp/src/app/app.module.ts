import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { TranslateLoader, TranslateModule, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { KniveDetailsComponent } from './knive-details/knive-details.component';
import { EditKniveComponent } from 'src/app/edit-knive/edit-knive.component';
import { KnivesService } from 'src/Services/knives.service';
import { FooterComponent } from './footer/footer.component';
import { PrivacyComponent } from './privacy/privacy.component';
import { ContactInfoComponent } from "./contact-info/contact-info.component";
import { KniveCardComponent } from './knive-card/knive-card.component';
import { OrderComponent } from './order/order.component';
import { OrderService } from '../Services/orders.service';
import { OrderSummaryComponent } from './order-summary/order-summary.component'
import { OrderManagerComponent } from './order-manager/order-manager.component';
import { LoadingScreenComponent } from './loading-screen/loading-screen.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    KniveDetailsComponent,
    EditKniveComponent,
    FooterComponent,
    PrivacyComponent,
    ContactInfoComponent,
    KniveCardComponent,
    OrderComponent,
    OrderSummaryComponent,
    OrderManagerComponent,
    LoadingScreenComponent
  ],
  imports: [
    HttpClientModule,
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
    RouterModule.forRoot([
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
        path: 'order-summary',
        component: OrderSummaryComponent
      },
      {
        path: 'order-manager',
        component: OrderManagerComponent,
        canActivate: [AuthorizeGuard],
        data: { isAuthenticated: true }
      },
      {
        path: 'edit-knive/:id',
        component: EditKniveComponent,
        canActivate: [AuthorizeGuard],
        data: { isAuthenticated: true }
      }
    ])
  ],
  providers: [
    { provide: AuthorizeGuard, useClass: AuthorizeGuard },
    { provide: TranslateService, useClass: TranslateService },
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: KnivesService, useClass: KnivesService },
    { provide: OrderService, useClass: OrderService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
