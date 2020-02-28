import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Order } from 'src/app/models/order';
import { SharedService } from 'src/app/services/shared-service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orderForm: FormGroup;
  submitted = false;
  order: Order = new Order();
  orderStatus: string;
  constructor(private fb: FormBuilder,
    private sharedService : SharedService,
    private orderService: OrderService
    ) { 
      this.createForm();
    }

  ngOnInit(): void {

  }

  createForm() {
    this.orderForm = this.fb.group({
      firstname: ['', Validators.required ],
      lastname: ['', Validators.required ],
      email: ['', [Validators.required, Validators.email] ]
    });
  }
  
  get f() { 
    return this.orderForm.controls;
   }
 
  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.orderForm.invalid) { 
        return;
    }

    if (this.orderForm.valid) {
      this.order.LineItems = [];
      var cartItems = this.sharedService.getCartItems();
      cartItems.forEach(item => {
        this.order.LineItems.push({productId: item.productId, quantity:item.quantity})
      });
      this.order.CustomerName = this.orderForm.controls['firstname'].value + ' ' + this.orderForm.controls['lastname'].value;
      this.order.CustomerEmail = this.orderForm.controls['email'].value;
      this.orderService.submit(this.order).then(res =>
        {
          this.orderStatus = res.toString();
        }
      );
      
    }
}
}
