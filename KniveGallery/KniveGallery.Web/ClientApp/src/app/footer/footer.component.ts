import { Component } from '@angular/core';
import { KnivesService } from '../../Services/knives.service'

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss']
})
/** footer component*/
export class FooterComponent {
    public adminEmail: string;
    public adminPhone: string;
    /** footer ctor */
    constructor(private knivesService: KnivesService) {
    }

    ngOnInit() {
        this.knivesService.getAdminDetails().subscribe((data: object)=>{
            this.adminEmail = data[0],
            this.adminPhone = data[1]
        });
    }
}
