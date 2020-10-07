/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { KniveDetailsComponent } from './knive-details.component';

let component: KniveDetailsComponent;
let fixture: ComponentFixture<KniveDetailsComponent>;

describe('knive-details component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ KniveDetailsComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(KniveDetailsComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
