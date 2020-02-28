import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Product } from "../models/product";
import { Observable } from "rxjs";

@Injectable()

export class ProductService
{
    private headers: HttpHeaders;
    public productList : any;

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
    }

public async get(){
    // Get all products
   return await this.http.get(environment.baseApiUrl + 'product/getAll' ,{observe: 'response' }).toPromise();
    }
}