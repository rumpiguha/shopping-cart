import { TestBed, getTestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { HttpEventType, HttpEvent } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { OrderService } from '../order.service';
import { Order } from 'src/app/models/order';

describe('OrderService', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [OrderService]
        });
    });

    it(
        'should post order',
        inject(
            [HttpTestingController, OrderService],
            (
                httpMock: HttpTestingController,
                dataService: OrderService
            ) => {
                const mockOrder: Order = {LineItems: [{productId: 'testid',quantity: 1}], CustomerEmail: 'test@test.com', CustomerName: 'test'};;

                const res: string = 'Order success'

                dataService.submit(mockOrder).then((event: HttpEvent<any>) => {
                    switch (event.type) {
                        case HttpEventType.Response:
                            expect(event.body).toEqual(res);
                    }
                });
                const mockReq = httpMock.expectOne(environment.baseApiUrl + 'order');

                expect(mockReq.cancelled).toBeFalsy();
                expect(mockReq.request.responseType).toEqual('json');
                mockReq.flush(mockOrder);

                httpMock.verify();
            }
        ));
});
