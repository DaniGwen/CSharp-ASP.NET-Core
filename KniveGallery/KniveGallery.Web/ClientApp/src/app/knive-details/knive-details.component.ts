import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { KnivesService } from '../../Services/knives.service';
import { Knive } from '../../Models/knive';

@Component({
  selector: 'app-knive-details',
  templateUrl: './knive-details.component.html',
  styleUrls: ['./knive-details.component.scss']
})
export class KniveDetailsComponent {

  kniveId: any;
  knive = new Knive();
  kniveImages: string[];
  isOrder: boolean = false;

  constructor(private route: ActivatedRoute,
    private knivesService: KnivesService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.kniveId = params.get('id');

      this.knivesService.getKniveById(this.kniveId).subscribe((data: any) => {
        this.knive = data;
      })

      this.knivesService.getKniveImages(this.kniveId).subscribe((data: any) => {
        this.kniveImages = data;
      });
    });
  }

  toggleOrder() {
    this.isOrder = !this.isOrder;
  }

  hideOrderHandler() {
    this.isOrder = false;
  }
}
