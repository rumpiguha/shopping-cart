import { async, ComponentFixture, TestBed, inject, tick } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing'
import { ProductListComponent } from './product-list.component';
import { ProductService } from 'src/app/services/product.service';
import { FxRateService } from 'src/app/services/fx-rate.service';
import { SharedService } from 'src/app/services/shared-service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Product } from 'src/app/models/product';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialog/dialog.component';
import { LineItem } from 'src/app/models/lineItem';

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productService: ProductService;
  let mockItems:Product[] = [{ name: 'testName1', productId: 'testId1', unitPrice: 1, description: 'testdescp1', maximumQuantity: 1 },
  { name: 'testName2', productId: 'testId2', unitPrice: 2, description: 'testdescp2', maximumQuantity: 2 }] ;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductListComponent, DialogComponent ],
      providers: [SharedService, ProductService, FxRateService],
      imports: [RouterTestingModule.withRoutes([]), HttpClientTestingModule, MatDialogModule],
    })
    .compileComponents();
    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
    component.products.data = mockItems;
    fixture.detectChanges();
  }));

  it('should create the app', () => {
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(component.products.data).toEqual(mockItems);
    })
  }); 

  it('Should update the totalCost when a new cart item is added', () => {

    let lineItemList = [{productId: "testId1", quantity: "2"}];
    let newItem = <LineItem>{productId: "testId2", quantity:3};
   // component.products.data = mockItems;
    component.lineItemList = lineItemList;
    component.totalCost = Number(lineItemList[0].quantity) * mockItems[0].unitPrice;

    component.quantityChanged({target: {value: newItem.quantity}}, newItem.productId);
    expect(component.lineItemList.length).toEqual(2);
    expect(component.totalCost).toEqual(Number(lineItemList[0].quantity) * mockItems[0].unitPrice + Number(lineItemList[1].quantity) * mockItems[1].unitPrice);
    
  });

  it('Should update the totalCost when quantity for an existing cart item quantity is updated', () => {

    let lineItemList = [{productId: "testId1", quantity: "2"}, {productId: "testId2", quantity: "3"}];
    let updatedItem = <LineItem>{productId: "testId2", quantity:4};
    //component.products.data = mockItems;
    component.lineItemList = lineItemList;
    component.totalCost = Number(lineItemList[0].quantity) * mockItems[0].unitPrice +  Number(lineItemList[1].quantity) * mockItems[1].unitPrice;

    component.quantityChanged({target: {value: updatedItem.quantity}}, updatedItem.productId);
    expect(component.lineItemList.length).toEqual(2);
    expect(component.lineItemList[1].quantity).toEqual(updatedItem.quantity);
    expect(component.totalCost).toEqual(Number(lineItemList[0].quantity) * mockItems[0].unitPrice + Number(lineItemList[1].quantity) * mockItems[1].unitPrice);
    
  });

  it('Should update the totalCost when an existing cart item is removed', () => {

    let lineItemList = [{productId: "testId1", quantity: "2"}, {productId: "testId2", quantity: "3"}];
    let removedItem = <LineItem>{productId: "testId2", quantity:0};
   
    component.lineItemList = lineItemList;
    component.totalCost = Number(lineItemList[0].quantity) * mockItems[0].unitPrice +  Number(lineItemList[1].quantity) * mockItems[1].unitPrice;

    component.quantityChanged({target: {value: removedItem.quantity}}, removedItem.productId);
    expect(component.lineItemList.length).toEqual(1);
    expect(component.totalCost).toEqual(Number(lineItemList[0].quantity) * mockItems[0].unitPrice);
    
  });

  it('should test the product table ', () => {
    
    expect(component.products.data).toEqual(mockItems);
    fixture.whenStable().then(() => {
      fixture.detectChanges();
      let tableHeaders = fixture.nativeElement.querySelector('mat-header-cell');
      expect(tableHeaders.length).toBe(4);
  
      // Header row
      let headerRow = tableHeaders[0];
      expect(headerRow.cells[0].innerHTML).toBe('Product');
      expect(headerRow.cells[1].innerHTML).toBe('Price');
      expect(headerRow.cells[2].innerHTML).toBe('Quantity');
  
      // Data rows
      let row1 = tableHeaders[1];
      expect(row1.cells[0].innerHTML).toBe('testName1');
      expect(row1.cells[1].innerHTML).toBe(1);
  
    });})
})
