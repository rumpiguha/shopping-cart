import { TestBed, getTestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/models/product';
import { HttpEventType, HttpEvent } from '@angular/common/http';
import { environment } from 'src/environments/environment';

describe('ProductService', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [ProductService]
        });
    });

    it(
        'should get products',
        inject(
            [HttpTestingController, ProductService],
            (
                httpMock: HttpTestingController,
                dataService: ProductService
            ) => {
                const mockProducts: Product[] = [
                    { name: 'testName1', productId: 'testId1', unitPrice: 1, description: 'testdescp1', maximumQuantity: 1 },
                    { name: 'testName2', productId: 'testId2', unitPrice: 2, description: 'testdescp2', maximumQuantity: 2 }
                ];

                dataService.get().then((event: HttpEvent<any>) => {
                    switch (event.type) {
                        case HttpEventType.Response:
                            expect(event.body).toEqual(mockProducts);
                    }
                });
                const mockReq = httpMock.expectOne(environment.baseApiUrl + 'product/getAll');

                expect(mockReq.cancelled).toBeFalsy();
                expect(mockReq.request.responseType).toEqual('json');
                mockReq.flush(mockProducts);

                httpMock.verify();
            }
        ));
});
