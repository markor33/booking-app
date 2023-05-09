from pymodm import MongoModel, fields
import uuid

class Address(MongoModel):
    country = fields.CharField()
    city = fields.CharField()
    street = fields.CharField()
    number = fields.CharField()

class DateRange(MongoModel):
    start_date = fields.DateTimeField()
    end_date = fields.DateTimeField()

class Reservation(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    period = fields.EmbeddedDocumentField(DateRange)

class Benefit(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    name = fields.CharField()

class Accommodation(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    hostId = fields.CharField()
    name = fields.CharField()
    location = fields.EmbeddedDocumentField(Address)
    minGuests = fields.IntegerField()
    maxGuests = fields.IntegerField()
    photo = fields.CharField()
    benefits = fields.EmbeddedDocumentListField(Benefit)
    reservations = fields.EmbeddedDocumentListField(Reservation)

    class Meta:
        connection_alias = 'search_db'
        collection_name = 'accommodations'
