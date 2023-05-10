from models.models import *

def search_accommodations(location, num_guests, start_date, end_date):
    accommodations = Accommodation.objects.raw({
        '$or': [
            { 'location.country': location },
            { 'location.city': location }
        ],
        'min_guests': {'$lte': num_guests},
        'max_guests': {'$gte': num_guests},
        '$nor': [
            {
                '$and': [
                    {'reservations.period.start_date': {'$lte': end_date}},
                    {'reservations.period.end_date': {'$gte': start_date}}
                ],
            }
        ]
    }).all()

    return accommodations
    #return [accommodation.to_son().to_dict() for accommodation in accommodations]
