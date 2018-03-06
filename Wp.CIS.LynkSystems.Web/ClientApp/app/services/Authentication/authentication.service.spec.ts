/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { DebugElement, NO_ERRORS_SCHEMA } from '@angular/core'
import { Http, ConnectionBackend, RequestOptions, HttpModule } from '@angular/http';

import { Observable } from "rxjs/Observable";
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import '../../app.module.client';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';

import { AuthenticationService, AuthGuard, TokenService, User } from './index';

let authService: AuthenticationService;
let http: Http;
describe('Authentication Service functionality', () => {

    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        let store = {};
        const mockLocalStorage = {
            getItem: (key: string): string => {
                return key in store ? store[key] : null;
            },
            setItem: (key: string, value: string) => {
                store[key] = `${value}`;
            },
            removeItem: (key: string) => {
                delete store[key];
            },
            clear: () => {
                store = {};
            }
        };

        spyOn(localStorage, 'getItem')
            .and.callFake(mockLocalStorage.getItem);
        spyOn(localStorage, 'setItem')
            .and.callFake(mockLocalStorage.setItem);
        spyOn(localStorage, 'removeItem')
            .and.callFake(mockLocalStorage.removeItem);
        spyOn(localStorage, 'clear')
            .and.callFake(mockLocalStorage.clear);

    }));

    it('should nullify the User Auth token from localStorage when logout is called.',
        () => {
            authService = new AuthenticationService(http);
            localStorage.setItem('WorldPay.cis.currentUserToken', 'anothertoken');

            //Act 
            authService.logout();
            expect(localStorage.getItem('WorldPay.cis.currentUserToken')).toEqual(null);
        });
});