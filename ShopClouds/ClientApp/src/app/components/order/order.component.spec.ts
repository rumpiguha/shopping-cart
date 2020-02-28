import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing'
import { OrderComponent } from './order.component';
import { OrderService } from 'src/app/services/order.service';
import { SharedService } from 'src/app/services/shared-service';
import { FormBuilder } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('OrderComponent', () => {
  let component: OrderComponent;
  let fixture: ComponentFixture<OrderComponent>;
  let orderService: OrderService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderComponent ],
      providers: [SharedService, OrderService, FormBuilder],
      imports: [HttpClientTestingModule],
    })
    .compileComponents();
    fixture = TestBed.createComponent(OrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));


  it('should create the app', () => {
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should submit order', () => {
    component.onSubmit();
    expect(component.submitted).toBeTruthy();
  });

  it('form invalid when empty', () => {
    expect(component.orderForm.valid).toBeFalsy();
  });
});
