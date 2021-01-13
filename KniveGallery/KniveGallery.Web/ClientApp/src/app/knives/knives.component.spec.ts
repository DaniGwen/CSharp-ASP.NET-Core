/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { KnivesComponent } from './knives.component';

let component: KnivesComponent;
let fixture: ComponentFixture<KnivesComponent>;

describe('knives component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ KnivesComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(KnivesComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});