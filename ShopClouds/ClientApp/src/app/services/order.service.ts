import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Order } from "../models/order";

@Injectable()

export class OrderService
{
    private headers: HttpHeaders;

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
    }

public async submit(payload: Order) {
    return await this.http.post(environment.baseApiUrl + 'order', payload, {headers: this.headers}).toPromise();
  }
}