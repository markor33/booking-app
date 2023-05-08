from models.models import *

def search_accommodations(country, city, num_guests, start_date, end_date):
    accommodations = Accommodation.objects.raw({
        '$or': [
            { 'location.country': country },
            { 'location.city': city }
        ],
        'minGuests': {'$lte': num_guests},
        'maxGuests': {'$gte': num_guests},
        '$nor': [
            {
                '$and': [
                    {'reservations.period.start_date': {'$lte': end_date}},
                    {'reservations.period.end_date': {'$gte': start_date}}
                ],
            }
        ]
    }).all()

    return [accommodation.to_son().to_dict() for accommodation in accommodations]
