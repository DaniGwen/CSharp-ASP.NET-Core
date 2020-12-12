/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { AddCarouselImagesComponent } from './add-carousel-images.component';

let component: AddCarouselImagesComponent;
let fixture: ComponentFixture<AddCarouselImagesComponent>;

describe('add-carousel-images component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ AddCarouselImagesComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(AddCarouselImagesComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});