import { PriceType } from "./price-type";

export class Price {

    id: string;
    amount: number;
    type: PriceType;

    constructor(priceInter: PriceInterface){
        this.id = priceInter.id;
        this.amount = priceInter.amount;
        this.type = priceInter.type;
    }

}

interface PriceInterface{
    id: string;
    amount: number;
    type: PriceType;
}