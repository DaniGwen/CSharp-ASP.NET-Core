import { Component, OnInit } from '@angular/core';
import { KnivesService } from '../../Services/knives.service';
import { HttpClient } from '@angular/common/http';
import { Knive } from '../../Models/knive';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { ShoppingCartService } from 'src/Services/shopping-cart.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  public knives: Knive[];
  public kniveCl: string;
  public isKniveDeleted = false;
  public showLoader: boolean = false;

  constructor(
    public http: HttpClient,
    private knivesService: KnivesService) {
  }

  ngOnInit() {
    this.getAllKnives();
  }

  onDelete(kniveId: number) {
    this.showLoader = true;
    this.knivesService.removeKnive(kniveId).subscribe(() => {
      this.isKniveDeleted = true;
      this.showLoader = false;
      this.getAllKnives();
    })

    setInterval(() => { this.isKniveDeleted = false }, 5000);
  }

  hideMessage() {
    this.isKniveDeleted = false;
  }

  getKnivesByClass(kniveClass: string) {
    this.showLoader = true;

    this.kniveCl = kniveClass;
    this.knivesService.getKnivesByClass(kniveClass)
      .subscribe((data: any) => {
        if (data) {
          this.showLoader = false;
          this.knives = data;
        }
      });
  }

  getAllKnives() {
    this.showLoader = true;
    this.kniveCl = 'All';

    this.knivesService.getAllknives()
      .subscribe((knives: Knive[]) => {
        if (knives) {
          this.knives = knives;
          this.showLoader = false;
        }
      });
  }
}
