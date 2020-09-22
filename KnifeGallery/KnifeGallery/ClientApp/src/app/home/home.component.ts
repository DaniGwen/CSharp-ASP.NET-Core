import { Component, OnInit } from '@angular/core';
import { KnivesService } from '../knives.service';
import { HttpClient } from '@angular/common/http';
import { Knive } from '../knive';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  kniveForm: any;
  knives: Knive[];
  kniveIdUpdate = null;

  constructor(public http: HttpClient, private knivesService: KnivesService) { }

  public ngOnInit() {
    this.knivesService.getAllknives().subscribe((data: any) => {
      this.knives = data;
    });
  }
}
