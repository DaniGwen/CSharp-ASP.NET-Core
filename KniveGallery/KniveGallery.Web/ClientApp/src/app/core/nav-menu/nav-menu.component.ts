import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Knive } from 'src/Models/knive';
import { ShoppingCartService } from 'src/Services/shopping-cart.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  itemsCount: number = 0;

  constructor(public translate: TranslateService,
    private cartService: ShoppingCartService) {
  }
  
  ngOnInit() {
    this.cartService.$itemsChange.subscribe((items: Knive[]) => {
      this.itemsCount = items.length;
    });
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
