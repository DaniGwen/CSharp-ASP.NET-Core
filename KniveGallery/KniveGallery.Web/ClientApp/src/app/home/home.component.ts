import { Component, OnInit } from '@angular/core';
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
  imagePath: string;
  public kniveCl: string;
  public isAuthenticated: Observable<boolean>;

  constructor(
    private authorizeService: AuthorizeService,
    public http: HttpClient,
    private knivesService: KnivesService) { }

  public ngOnInit() {
    this.kniveCl = "All";
    
    this.knivesService.getAllknives()
      .subscribe((data: any) => {
        this.knives = data;
      });

    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  deleteKnive(kniveId: number) {
    this.knivesService.removeKnive(kniveId).subscribe(data => {
      this.ngOnInit();
    });
  }

  getKnivesByClass(kniveClass: string) {
    this.kniveCl = kniveClass;
    this.knivesService.getKnivesByClass(kniveClass)
      .subscribe((data: any) => {
        this.knives = data;
      });
  }
}
