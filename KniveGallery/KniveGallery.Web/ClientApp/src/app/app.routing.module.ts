import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
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


const routes: Routes = [
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
        data: {
            title: '404'
        }
    }
];

export const AppRoutingModule = RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' });
