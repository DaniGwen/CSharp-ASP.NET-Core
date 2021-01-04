import {  Component, OnInit } from '@angular/core';
import { KnivesService } from '../../../Services/knives.service'

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements  OnInit {

  public adminEmail: string;
  public adminPhone: string;

  constructor(private knivesService: KnivesService) {
  }

  ngOnInit() {
    this.knivesService.getAdminDetails().subscribe((data: any) => {
      if (data) {
        this.adminEmail = data.email,
        this.adminPhone = data.phoneNumber
      }
    });
  }
}
