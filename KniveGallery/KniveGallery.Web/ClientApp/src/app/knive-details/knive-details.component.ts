import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { KnivesService } from '../../Services/knives.service';
import { Knive } from '../../Models/knive';

@Component({
  selector: 'app-knive-details',
  templateUrl: './knive-details.component.html',
  styleUrls: ['./knive-details.component.scss']
})
/** knife-details component*/
export class KniveDetailsComponent {

  kniveId: any;
  knive: Knive;

  constructor(private route: ActivatedRoute,
    private knivesService: KnivesService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.kniveId = params.get('id');

      this.knivesService.getKniveById(this.kniveId).subscribe((data: any) => {
        this.knive = data
      })
    });
  }
}
