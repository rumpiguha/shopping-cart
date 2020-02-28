import { LineItem } from "./lineItem";

export class Order {
    public LineItems: LineItem[];
    public CustomerEmail: string;
    public CustomerName: string;
}