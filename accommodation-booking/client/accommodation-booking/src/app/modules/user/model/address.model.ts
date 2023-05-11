export class Address {

    country: string;
    city: string;
    street: string;
    number: string;

    constructor(addressInter: AddressInterface){
        this.country = addressInter.country;
        this.city = addressInter.city;
        this.street = addressInter.street;
        this.number = addressInter.number;
    }
}

interface AddressInterface{
    country: string;
    city: string;
    street: string;
    number: string;
}
