import { Component } from '@angular/core';
import { KnivesService } from '../../Services/knives.service';

@Component({
  selector: 'app-contact-info',
  templateUrl: './contact-info.component.html',
  styleUrls: ['./contact-info.component.scss']
})
export class ContactInfoComponent {

  public mobile: string;
  public email: string;

  constructor(private knivesService: KnivesService) { }

  ngOnInit() {
    this.knivesService.getAdminDetails().subscribe((data: any) => {
      this.email = data.email;
      this.mobile = data.phoneNumber;
    });
  }
}
