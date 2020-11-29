"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
var testing_1 = require("@angular/core/testing");
var platform_browser_1 = require("@angular/platform-browser");
var order_component_1 = require("./order.component");
var component;
var fixture;
describe('order component', function () {
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [order_component_1.OrderComponent],
            imports: [platform_browser_1.BrowserModule],
            providers: [
                { provide: testing_1.ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = testing_1.TestBed.createComponent(order_component_1.OrderComponent);
        component = fixture.componentInstance;
    }));
    it('should do something', testing_1.async(function () {
        expect(true).toEqual(true);
    }));
});
//# sourceMappingURL=order.component.spec.js.map