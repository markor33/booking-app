
class AccommodationDTO:
    def __init__(self, accommodation, num_guests, start_date, end_date):
        self.id = accommodation.id
        self.hostId = accommodation.host_id
        self.name = accommodation.name
        self.description = accommodation.description
        self.location = accommodation.location.to_son().to_dict()
        self.minGuests = accommodation.min_guests
        self.maxGuests = accommodation.max_guests
        self.photo = accommodation.photo
        self.benefits = [benefit.to_son().to_dict() for benefit in accommodation.benefits]
        self.priceType = accommodation.price_type
        self.price = accommodation.calculate_price(num_guests, start_date, end_date)

    def list_to_DTO(accommodation_list, num_guests, start_date, end_date):
        return [AccommodationDTO(accommodation, num_guests, start_date, end_date).__dict__ for accommodation in accommodation_list]
