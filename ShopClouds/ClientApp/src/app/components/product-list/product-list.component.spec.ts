import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing'
import { ProductListComponent } from './product-list.component';
import { ProductService } from 'src/app/services/product.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FxRateService } from 'src/app/services/fx-rate.service';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/services/shared-service';
import { Route } from '@angular/compiler/src/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Product } from 'src/app/models/product';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialog/dialog.component';

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productService: ProductService;
  let mockItems:ArrayLike<Product[]> = [] ;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductListComponent, DialogComponent ],
      providers: [SharedService, ProductService, FxRateService],
      imports: [RouterTestingModule.withRoutes([]), HttpClientTestingModule, MatDialogModule],
    })
    .compileComponents();
    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create the app', () => {
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(component.products.data).toEqual(mockItems)
    })
  }); 
});
