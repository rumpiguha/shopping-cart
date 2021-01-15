import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Currency } from './models/enum/currency';
import { SharedService } from './services/shared-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private sharedService: SharedService,
    private router: Router) { }
  title = 'app';

  keys = Object.keys;
  currencies = Currency;
  public selectedCurrency: any;
  ngOnInit(): void {
   this.sharedService.setCurrency(Currency.AUD);
  }
  onChange(newCurrency: any)
  {
    this.sharedService.setCurrency(newCurrency);
  }
}
