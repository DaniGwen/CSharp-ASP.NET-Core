"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var knives_service_1 = require("./knives.service");
describe('KnivesService', function () {
    var service;
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({});
        service = testing_1.TestBed.inject(knives_service_1.knivesService);
    });
    it('should be created', function () {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=knives.service.spec.js.map