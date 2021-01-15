import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from "@angular/material/table";
import {MatDialogModule} from "@angular/material/dialog";
import { AppComponent } from './app.component';
import { ProductService } from './services/product.service';
import { OrderService } from './services/order.service';
import { ProductListComponent } from './components/product-list/product-list.component';
import { CartComponent } from './components/cart/cart.component';
import { EnumKeyValueListPipe } from './enumlist.pipe';
import { FxRateService } from './services/fx-rate.service';
import { OrderComponent } from './components/order/order.component';
import { DialogComponent } from './dialog/dialog.component';
import { OnlynumberDirective } from './shared/number.directive';

const appRoutes: Routes = [
  {
    path: 'product',
    component: ProductListComponent
  },
  {
    path: 'cart',
    component: CartComponent
  },
  {
    path: 'order',
    component: OrderComponent
  },
  { path: '',
    redirectTo: '/product',
    pathMatch: 'full'
  }
];
@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    EnumKeyValueListPipe,
    CartComponent,
    OrderComponent,
    DialogComponent,
    OnlynumberDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(
     appRoutes
    )
  ],
  providers: [
    ProductService,
    FxRateService,
    OrderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
