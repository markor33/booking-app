import uuid
from datetime import datetime
from . import accommodation_search_pb2
from . import accommodation_search_pb2_grpc
from models import Accommodation, Address, Benefit, PriceInterval, DateRange

class AccommodationService(accommodation_search_pb2_grpc.AccommodationSearchServicer):
    def CreateAccommodation(self, request, context):
        accommodation = Accommodation(
            id = request.id,
            host_id = request.hostId,
            name = request.name,
            description = request.description,
            location = Address(
                country = request.location.country,
                city = request.location.city,
                street = request.location.street,
                number = request.location.number
            ),
            min_guests = request.minGuests,
            max_guests = request.maxGuests,
            photo = request.photo,
            price_type = request.priceType,
            general_price = request.generalPrice,
            weekend_increase = request.weekendIncrease
        )
        for benefit in request.benefits:
            accommodation.benefits.append(Benefit(
                id = benefit.id,
                name = benefit.name
            ))
        accommodation.save()
        response = accommodation_search_pb2.CreateAccommodationResponse()
        return response

    def CreatePriceInterval(self, request, context):
        accommodation = Accommodation.objects.get({'_id': uuid.UUID(request.accommodationId)})
        price_interval = PriceInterval(
            id = request.id,
            amount = request.amount,
            interval = DateRange(
                start_date = datetime.fromtimestamp(request.startDate.seconds),
                end_date = datetime.fromtimestamp(request.endDate.seconds)
            )
        )
        accommodation.price_intervals.append(price_interval)
        accommodation.save()
        response = accommodation_search_pb2.CreatePriceIntervalResponse()
        return response
