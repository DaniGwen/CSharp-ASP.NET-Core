import { Component, OnInit } from '@angular/core';
import { KnivesService } from 'src/Services/knives.service';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.component.html',
  styleUrls: ['./contact-page.component.scss']
})
export class ContactPageComponent implements OnInit {

  public adminEmail;
  public adminPhone;

  constructor(private kniveService: KnivesService) { }

  ngOnInit(): void {
    this.kniveService.getAdminDetails().subscribe((data: any) => {
      if (data) {
        this.adminEmail = data.email,
          this.adminPhone = data.phoneNumber
      }
    });
  }
}
