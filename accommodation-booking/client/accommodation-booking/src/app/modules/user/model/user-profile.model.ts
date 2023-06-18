import { Address } from "./address.model";

export class UserProfile {
    email: string;
    firstName: string;
    lastName: string;
    address: Address;
    flightBookingApiKey: string | null;

    constructor(userInter: UserProfileInterface){
        this.email = userInter.email;
        this.firstName = userInter.firstName;
        this.lastName = userInter.lastName
        this.address = userInter.address
        this.flightBookingApiKey = userInter.flightBookingApiKey;
    }
}

interface UserProfileInterface{
    email: string;
    firstName: string;
    lastName: string;
    address: Address;
    flightBookingApiKey: string | null;
}
