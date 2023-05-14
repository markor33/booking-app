import { Address } from "./address.model";

export class User {
    username: string = '';
    email: string = '';
    firstName: string = '';
    lastName: string = '';
    address: Address = new Address();
    type: UserType = UserType.GUEST;
}

export enum UserType {
    HOST,
    GUEST
}