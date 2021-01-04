import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import {
  Router,
  Event as RouterEvent,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError
} from '@angular/router'

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})
export class AppComponent {
  public showOverlay: boolean = true;
  title = 'app';

  constructor(private translate: TranslateService,
    private router: Router) {
    this.router.events.subscribe((event: RouterEvent) => {
      this.navigationInterceptor(event)
    });

    this.translate.addLangs(['EN', 'BG']);
    this.translate.setDefaultLang('BG');

    const browserLang = this.translate.getBrowserLang();
    this.translate.use(browserLang.match(/EN|BG/) ? browserLang : 'BG');
  }

  // Shows and hides the loading spinner during RouterEvent changes
  navigationInterceptor(event: RouterEvent): void {
    if (event instanceof NavigationStart) {
      this.showOverlay = true;
    }
    if (event instanceof NavigationEnd) {
      this.showOverlay = false;
    }

    // Set loading state to false in both of the below events to hide the spinner in case a request fails
    if (event instanceof NavigationCancel) {
      this.showOverlay = false;
    }
    if (event instanceof NavigationError) {
      this.showOverlay = false;
    }
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }
}
