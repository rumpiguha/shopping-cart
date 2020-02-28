import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Product } from "../models/product";
import { Observable } from "rxjs";
import { FxRate } from "../models/fxrate";

@Injectable()

export class FxRateService
{
    private headers: HttpHeaders;
    public ratesList : Array<FxRate>;

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
    }

public async get(){
    // Get all products
   return await this.http.get(environment.baseApiUrl + 'rate/getRates' ,{observe: 'response' }).toPromise();
    }
}