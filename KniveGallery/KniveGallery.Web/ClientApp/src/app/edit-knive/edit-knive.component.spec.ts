/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { EditKniveComponent } from './edit-knive.component';

let component: EditKniveComponent;
let fixture: ComponentFixture<EditKniveComponent>;

describe('edit-knive component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ EditKniveComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(EditKniveComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});