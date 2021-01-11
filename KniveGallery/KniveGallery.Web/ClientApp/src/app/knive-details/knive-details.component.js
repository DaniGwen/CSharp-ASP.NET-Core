"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.KniveDetailsComponent = void 0;
var core_1 = require("@angular/core");
var knive_1 = require("../../Models/knive");
var KniveDetailsComponent = /** @class */ (function () {
    function KniveDetailsComponent(route, knivesService) {
        this.route = route;
        this.knivesService = knivesService;
        this.knive = new knive_1.Knive();
        this.kniveImages = Array();
        this.hideImageGrid = "d-block";
    }
    KniveDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.showLoader = true;
        this.route.paramMap.subscribe(function (params) {
            _this.kniveId = params.get('id');
            _this.knivesService.getKniveById(_this.kniveId).subscribe(function (data) {
                _this.knive = data;
            });
            _this.knivesService.getKniveImages(_this.kniveId).subscribe(function (data) {
                _this.showLoader = false;
                _this.kniveImages = data;
            });
        });
        if (this.kniveImages == null || this.kniveImages.length == 0) {
            this.hideImageGrid = "d-none";
        }
    };
    KniveDetailsComponent = __decorate([
        core_1.Component({
            selector: 'app-knive-details',
            templateUrl: './knive-details.component.html',
            styleUrls: ['./knive-details.component.scss']
        })
    ], KniveDetailsComponent);
    return KniveDetailsComponent;
}());
exports.KniveDetailsComponent = KniveDetailsComponent;
//# sourceMappingURL=knive-details.component.js.map