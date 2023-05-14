import { Address } from "./address.model";
import { UserType } from "./user.model";

export class RegisterRequest {
    username: string = '';
    email: string = '';
    password: string = '';
    firstName: string = '';
    lastName: string = '';
    address: Address = new Address();
    userType: UserType = UserType.HOST;
}