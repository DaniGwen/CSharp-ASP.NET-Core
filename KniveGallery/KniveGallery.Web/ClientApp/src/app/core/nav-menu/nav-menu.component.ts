import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizeService } from "../../../api-authorization/authorize.service";
import { ShoppingCartService } from "../../../Services/shopping-cart.service";
import { Knive } from "../../../Models/knive";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = true;
  itemsCount: number = 0;
  isAuthenticated: boolean;

  constructor(public translate: TranslateService,
    private cartService: ShoppingCartService,
    private authService: AuthorizeService) {
  }

  ngOnInit() {
    this.cartService.$itemsChange.subscribe((items: Knive[]) => {
      if (items) {
        this.itemsCount = items.length;
      }
    });
    this.authService.isAuthenticated().subscribe((isAuth: boolean) => {
      this.isAuthenticated = isAuth;
    });
    if (this.isAuthenticated) {
      this.isExpanded = false;
    }
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }
}
