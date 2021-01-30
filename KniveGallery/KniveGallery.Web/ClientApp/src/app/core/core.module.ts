import { CommonModule } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { FooterComponent } from "./footer/footer.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { ApiAuthorizationModule } from "../../api-authorization/api-authorization.module";


@NgModule({
  declarations: [
    NavMenuComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ApiAuthorizationModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
  ],
  exports: [
    NavMenuComponent,
    FooterComponent
  ],
})
export class CoreModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
