import { Injectable } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { LineItem } from "../models/lineItem";

@Injectable({ providedIn: 'root' })
export class SharedService {
    private subjectCurrency = new Subject<any>();

    private cartItems: Array<any> = [];

    setCurrency(currency: string) {
        this.subjectCurrency.next({currency});
    }

    getCurrency(): Observable<any> {
        return this.subjectCurrency.asObservable();
    }

    setCartItems(cartItems: Array<any>){
        this.cartItems = cartItems;
    }

    getCartItems(): Array<any>{
        return this.cartItems;
    }
}