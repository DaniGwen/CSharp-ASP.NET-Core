import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private translate: TranslateService) {
    this.translate.addLangs(['en', 'bg']);
    this.translate.setDefaultLang('bg');

    const browserLang = this.translate.getBrowserLang();
    this.translate.use(browserLang.match(/en|bg/) ? browserLang : 'en');
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }
}
