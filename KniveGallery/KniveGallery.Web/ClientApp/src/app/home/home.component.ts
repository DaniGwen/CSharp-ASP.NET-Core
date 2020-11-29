import { Component, OnInit, AfterViewInit } from '@angular/core';
import { KnivesService } from '../../Services/knives.service';
import { HttpClient } from '@angular/common/http';
import { Knive } from '../../Models/knive';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  public knives: Knive[];
  public kniveCl: string;
  public isAuthenticated: Observable<boolean>;
  public isKniveDeleted = false;
  public showLoader: boolean;

  constructor(
    private authorizeService: AuthorizeService,
    public http: HttpClient,
    private knivesService: KnivesService) {
  }

  ngOnInit() {
    this.showLoader = true;

    this.kniveCl = 'All';

    this.knivesService.getAllknives()
      .subscribe((knives: Knive[]) => {
        if (knives) {
          this.showLoader = false;
        }
        this.knives = knives;
      });

    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  onDelete(kniveId: number) {
    this.showLoader = true;
    this.knivesService.removeKnive(kniveId).subscribe(() => {
      this.isKniveDeleted = true;
      this.ngOnInit();
    })
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
        }
        this.knives = data;
      });
  }
}
