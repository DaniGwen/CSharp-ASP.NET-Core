import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Knive } from 'src/Models/knive';
import { KnivesService } from 'src/Services/knives.service';

@Component({
    selector: 'app-knives',
    templateUrl: './knives.component.html',
    styleUrls: ['./knives.component.scss']
})
export class KnivesComponent {
    public knives: Array<Knive> = new Array();
    public kniveCl: string;
    public isKniveDeleted = false;
    public showLoader: boolean = false;
    constructor(private knivesService: KnivesService,
        private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            this.kniveCl = params.get('kniveClass');
        })
        this.getKnivesByClass(this.kniveCl);

        //DEMO 
        //this.knives = [{
        //    edgeLength: 100,
        //    edgeMade: "TestTest",
        //    edgeThickness: 3,
        //    edgeWidth: 10,
        //    handleDescription: "SomeDescription for handle",
        //    imagePath: "high1.jpg",
        //    kniveId: 10,
        //    price: 50,
        //    quantity: 2,
        //    totalLength: 230,
        //    likes: 0,
        //    quantityOrdered: 0
        //},
        //{
        //    edgeLength: 100,
        //    edgeMade: "TestTest",
        //    edgeThickness: 3,
        //    edgeWidth: 10,
        //    handleDescription: "SomeDescription for handle",
        //    imagePath: "middle2.jpg",
        //    kniveId: 10,
        //    price: 50,
        //    quantity: 2,
        //    totalLength: 230,
        //    likes: 0,
        //    quantityOrdered: 0
        //},
        //{
        //    edgeLength: 100,
        //    edgeMade: "TestTest",
        //    edgeThickness: 3,
        //    edgeWidth: 10,
        //    handleDescription: "SomeDescription for handle",
        //    imagePath: "kitchen2.jpg",
        //    kniveId: 10,
        //    price: 50,
        //    quantity: 2,
        //    totalLength: 230,
        //    likes: 0,
        //    quantityOrdered: 0
        //}];
    }

    onDelete(kniveId: number) {
        this.showLoader = true;
        this.knivesService.removeKnive(kniveId).subscribe(() => {
            this.isKniveDeleted = true;
            this.showLoader = false;
            this.getKnivesByClass(this.kniveCl);
        })
        setInterval(() => { this.isKniveDeleted = false }, 5000);
    }

    hideMessage() {
        this.isKniveDeleted = false;
    }

    getKnivesByClass(kniveClass: string) {
        this.showLoader = true;
        this.knivesService.getKnivesByClass(kniveClass)
            .subscribe((data: any) => {
                if (data) {
                    this.showLoader = false;
                    this.knives = data;
                }
            });
    }
}
