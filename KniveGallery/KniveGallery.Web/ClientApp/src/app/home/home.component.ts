import { Component, OnInit } from '@angular/core';
import { KnivesService } from '../../Services/knives.service';
import { HttpClient } from '@angular/common/http';
import { Knive } from '../../Models/knive';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  public knives: Knive[];
  imagePath: string;
  public isAuthenticated: Observable<boolean>;

  constructor(private authorizeService: AuthorizeService,
    public http: HttpClient,
    private knivesService: KnivesService) { }

  public ngOnInit() {
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
}
