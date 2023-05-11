export class Accommodation {
    
    name: string;
    photo: string;

    constructor(accomInter: AccommodationInterface){
        this.name = accomInter.name;
        this.photo = accomInter.photo;
    }
}

interface AccommodationInterface{
    name: string;
    photo: string;
}