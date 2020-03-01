import { Component, OnInit, DoCheck, AfterViewInit } from '@angular/core';
import { SharedService } from 'src/app/services/shared-service';
import { LineItem } from 'src/app/models/lineItem';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  public finalItems = new MatTableDataSource<any>();
  public displayedColumns = ['name', 'quantity', 'totalprice'];
  public totalCost:number = 0;
  public constructor(
    private sharedService : SharedService,
    private router: Router) { }

  ngOnInit() {
    this.finalItems.data =  this.sharedService.getCartItems();
    if(!!this.finalItems.data){
      this.totalCost = this.finalItems.data.reduce((sum, cur) => sum + cur.totalprice, 0);
    }
  }
 
    goBack()
    {
      this.router.navigate(['product']);
    }

    checkOut()
    {
      this.router.navigate(['order']);
    }
}
