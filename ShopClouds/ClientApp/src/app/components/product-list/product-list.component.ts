import { Component, OnInit, Input, DoCheck, OnDestroy } from '@angular/core';
import { Product } from 'src/app/models/product';
import { Observable, Subscription } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { MatTableDataSource } from '@angular/material/table';
import { LineItem } from 'src/app/models/lineItem';
import { Order } from 'src/app/models/order';
import { SharedService } from 'src/app/services/shared-service';
import { Currency } from 'src/app/models/enum/currency';
import { Router } from '@angular/router';
import { FxRateService } from 'src/app/services/fx-rate.service';
import { FxRate } from 'src/app/models/fxrate';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialog/dialog.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnDestroy {

  selectedCurrency: any;
  descriptionDialogRef: MatDialogRef<DialogComponent>;

  public products = new MatTableDataSource<Product[]>();
  public lineItem = new LineItem;
  public selectedProduct: Product;
  public lineItemList = [];
  public totalCost: number = 0;
  public displayedColumns = ['name', 'unitPrice', 'maximumQuantity', 'view']
  private currencySubscription: Subscription;
  public constructor(
    private productService: ProductService,
    private fxRateService: FxRateService,
    private sharedService : SharedService,
    public dialog: MatDialog,
    private router: Router) { }

  async ngOnInit() {
    var data = (await this.productService.get()).body;
    if (!!data) {
      this.products.data = Object.keys(data).map(it => data[it]);
      this.products.data.forEach(item => {
        item['unitPrice'] =Number((item['unitPrice'] * .2) + item['unitPrice']);
      });
    }
    this.currencySubscription = this.sharedService.getCurrency().subscribe(
      async x => {
        let sourceCurrency = this.selectedCurrency || Currency.AUD;
        this.selectedCurrency = x['currency']; 
        var ratedata = (await this.fxRateService.get()).body;
        if (!!ratedata) {
          let rates:Array<FxRate> = Object.keys(ratedata).map(it => ratedata[it]);
          let rateObj = rates.find(rate => rate.sourceCurrency === sourceCurrency && rate.targetCurrency === this.selectedCurrency);
         if(!!rateObj)
         {
          this.products.data.forEach(p => {
            p['unitPrice'] = Number((p['unitPrice'] * rateObj.rate));
          });
          this.totalCost = (this.totalCost > 0)? this.totalCost * rateObj.rate : 0;
         }
         else if(sourceCurrency === Currency.GBP && this.selectedCurrency === Currency.USD)
         {
          this.products.data.forEach(p => {
            p['unitPrice'] = Number((p['unitPrice'] * 1.3));
          });
          this.totalCost = (this.totalCost > 0)? this.totalCost *  1.3 : 0;
         }
         else if(sourceCurrency === Currency.USD && this.selectedCurrency === Currency.GBP)
         {
          this.products.data.forEach(p => {
            p['unitPrice'] = Number((p['unitPrice'] * 0.77));
          });
          this.totalCost = (this.totalCost > 0)? this.totalCost *  0.77 : 0;
         }
        }    
      });
  }


  quantityChanged(e: { target: { value: number| string; }; }, id: string) {
    var value = (e.target.value == "")? 0 : e.target.value;
    this.lineItem.quantity = Number(value);
    this.lineItem.productId = id;
    var index = this.lineItemList.findIndex(x => x.productId === id);
    if (index !== -1) {
      this.lineItemList.splice(index, 1);
    }
    
    if(this.lineItem.quantity > 0)
    {
      this.lineItemList.push({productId: id, quantity: this.lineItem.quantity});
    }
  
    this.calculatePrice();
  }

  calculatePrice() {
    this.totalCost = 0;
    if (!!this.lineItemList && this.lineItemList.length > 0) {
      this.lineItemList.forEach(item => {
        var selectedItem = this.products.data.filter(x => x['productId'] === item.productId);
        var unitPrice = selectedItem[0]['unitPrice'];
        var itemTotalPrice = Number(unitPrice * item.quantity);
        this.totalCost += itemTotalPrice;
        this.lineItemList.find(x => x.productId == item.productId).totalprice = itemTotalPrice;
        this.lineItemList.find(x => x.productId == item.productId).name = selectedItem[0]['name'];
        this.lineItemList.find(x => x.productId == item.productId).cartTotal =  this.totalCost;
      })
    }
    this.sharedService.setCartItems(this.lineItemList);
  }

  goToCart(){
    this.router.navigate(['cart']);
  }

  openDialog(item: { name: any; description: any; })
  {
    this.descriptionDialogRef = this.dialog.open(DialogComponent, {
      data: {
        name: item.name,
        description: item.description
      },
      height: '38%',
      width: '40%'
    });
  }

  ngOnDestroy(){
    this.currencySubscription.unsubscribe();
  }
}
