export class Credentials {
    
    email: string;
    userName:string;
    oldPassword: string;
    newPassword: string;

    constructor(credInter: CredentialsInterface){
        this.email = credInter.email;
        this.userName = credInter.userName;
        this.oldPassword = credInter.oldPassword;
        this.newPassword = credInter.newPassword;
    }
}

interface CredentialsInterface{
    email: string;
    userName:string;
    oldPassword: string;
    newPassword: string;
}