import { User } from "../../shared/user";

export class RegisterRequest {
    username: string = '';
    email: string = '';
    password: string = '';
    user: User = new User();
}