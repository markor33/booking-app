export class AccommodationCard {
    
    name: string;
    photoUrl: string;

    constructor(accomInter: AccommodationInterface){
        this.name = accomInter.name;
        this.photoUrl = accomInter.photoUrl;
    }
}

interface AccommodationInterface{
    name: string;
    photoUrl: string;
}