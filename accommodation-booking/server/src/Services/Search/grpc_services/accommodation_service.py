import sys
import uuid
from datetime import datetime
from . import accommodation_search_pb2
from . import accommodation_search_pb2_grpc
from models import Accommodation, Address, Benefit, PriceInterval, DateRange, Reservation

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

    def CreateReservation(self, request, context):
        accommodation = Accommodation.objects.get({'_id': uuid.UUID(request.accommodationId)})
        reservation = Reservation(
            id = request.id,
            period = DateRange(
                start_date = datetime.fromtimestamp(request.startDate.seconds),
                end_date = datetime.fromtimestamp(request.endDate.seconds)
            )
        )
        accommodation.reservations.append(reservation)
        accommodation.save()
        response = accommodation_search_pb2.CreateReservationResponse()
        return response

    def DeleteReservation(self, request, context):
        accommodation = Accommodation.objects.get({'_id': uuid.UUID(request.accommodationId)})
        for reservation in accommodation.reservations:
            if uuid.UUID(request.id) == reservation.id:
                print(reservation.id, file=sys.stderr)
                accommodation.reservations.remove(reservation)
                break
        print(accommodation.name, file=sys.stderr)
        accommodation.save()
        #Accommodation.objects.raw({"_id": uuid.UUID(request.accommodationId)}).update({"$pull": {"reservations": {"_id": uuid.UUID(request.id)}}})
        response = accommodation_search_pb2.DeleteReservationResponse()
        return response
