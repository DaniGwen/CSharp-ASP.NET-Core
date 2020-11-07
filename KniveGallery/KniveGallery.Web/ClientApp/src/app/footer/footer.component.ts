import { Component } from '@angular/core';
import { KnivesService } from '../../Services/knives.service'

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  public adminEmail: string;
  public adminPhone: string;

  constructor(private knivesService: KnivesService) {
  }

  ngOnInit() {
    this.knivesService.getAdminDetails().subscribe((data: any) => {
      this.adminEmail = data.email,
        this.adminPhone = data.phoneNumber
    });
  }
}
