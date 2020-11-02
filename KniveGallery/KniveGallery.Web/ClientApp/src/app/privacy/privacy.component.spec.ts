/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { PrivacyComponent } from './privacy.component';

let component: PrivacyComponent;
let fixture: ComponentFixture<PrivacyComponent>;

describe('privacy component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ PrivacyComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(PrivacyComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});