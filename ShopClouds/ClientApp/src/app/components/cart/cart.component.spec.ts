import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing'
import { CartComponent } from './cart.component';
import { SharedService } from 'src/app/services/shared-service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

describe('CartComponent', () => {
  let fixture: ComponentFixture<CartComponent>;
  let router: Router;
  let mockItems: Array<any> = [{productId: "testId", quantity: 1}] ;
  let component: CartComponent;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [SharedService],
      declarations: [ CartComponent ],
      imports: [RouterTestingModule.withRoutes([])],
    })
    .compileComponents();
    fixture = TestBed.createComponent(CartComponent);
    router = TestBed.get(Router);
  }));

  it('should create the app and data OnNgInit', () => {
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(component.finalItems.data).toEqual(mockItems)
    })
  });

  it('goBack', () => {
    const app = fixture.debugElement.componentInstance;
    const navigateSpy = spyOn(router, 'navigate');
    app.goBack();
    expect(navigateSpy).toHaveBeenCalledWith(['product']);
  });

  it('checkOut', () => {
    const app = fixture.debugElement.componentInstance;
    const navigateSpy = spyOn(router, 'navigate');
    app.checkOut();
    expect(navigateSpy).toHaveBeenCalledWith(['order']);
  });
});
