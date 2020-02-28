import { TestBed, getTestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { FxRateService } from 'src/app/services/fx-rate.service';
import { HttpEventType, HttpEvent } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FxRate } from 'src/app/models/fxrate';

describe('FxRateService', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [FxRateService]
        });
    });

    it(
        'should get rates',
        inject(
            [HttpTestingController, FxRateService],
            (
                httpMock: HttpTestingController,
                dataService: FxRateService
            ) => {
                const mockRates: FxRate[] = [
                    { sourceCurrency: 'src1', targetCurrency: 'tgt1', rate: 1 },
                    { sourceCurrency: 'src2', targetCurrency: 'tgt2', rate: 2}
                ];

                dataService.get().then((event: HttpEvent<any>) => {
                    switch (event.type) {
                        case HttpEventType.Response:
                            expect(event.body).toEqual(mockRates);
                    }
                });
                const mockReq = httpMock.expectOne(environment.baseApiUrl + 'rate/getRates');

                expect(mockReq.cancelled).toBeFalsy();
                expect(mockReq.request.responseType).toEqual('json');
                mockReq.flush(mockRates);

                httpMock.verify();
            }
        ));
});
