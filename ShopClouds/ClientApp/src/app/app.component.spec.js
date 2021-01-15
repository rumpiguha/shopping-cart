"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var shared_service_1 = require("./services/shared-service");
var testing_2 = require("@angular/router/testing");
describe('AppComponent', function () {
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [testing_2.RouterTestingModule],
            declarations: [
                app_component_1.AppComponent
            ],
            providers: [shared_service_1.SharedService]
        }).compileComponents();
    }));
    it('should create the app', testing_1.async(testing_1.inject([shared_service_1.SharedService, router_1.Router], function (sharedService, router) {
        var fixture = testing_1.TestBed.createComponent(app_component_1.AppComponent);
        var app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    })));
});
//# sourceMappingURL=app.component.spec.js.map