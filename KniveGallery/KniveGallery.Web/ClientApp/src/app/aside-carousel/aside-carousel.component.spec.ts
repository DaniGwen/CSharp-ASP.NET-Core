/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { AsideCarouselComponent } from './aside-carousel.component';

let component: AsideCarouselComponent;
let fixture: ComponentFixture<AsideCarouselComponent>;

describe('aside-carousel component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ AsideCarouselComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(AsideCarouselComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});