import uuid
from pymodm import MongoModel, fields
from datetime import datetime, timedelta

class Address(MongoModel):
    country = fields.CharField()
    city = fields.CharField()
    street = fields.CharField()
    number = fields.CharField()

class DateRange(MongoModel):
    start_date = fields.DateTimeField()
    end_date = fields.DateTimeField()

    def get_number_of_weekends(start_date, end_date):
        days_difference = (end_date - start_date).days
        full_weeks, remaining_days = divmod(days_difference, 7)

        weekends_count = full_weeks * 2

        last_full_week_start = start_date + timedelta(weeks=full_weeks)
        for i in range(remaining_days + 1):
            current_date = last_full_week_start + timedelta(days=i)
            if current_date.weekday() in (5, 6):
                weekends_count += 1

        return weekends_count

class Reservation(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    period = fields.EmbeddedDocumentField(DateRange)

class Benefit(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    name = fields.CharField()

class PriceType():
    PER_GUEST = 0
    IN_WHOLE = 1

class PriceInterval(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    amount = fields.FloatField()
    interval = fields.EmbeddedDocumentField(DateRange)

class Accommodation(MongoModel):
    id = fields.UUIDField(primary_key=True, default=uuid.uuid4)
    host_id = fields.CharField()
    name = fields.CharField()
    description = fields.CharField()
    location = fields.EmbeddedDocumentField(Address)
    min_guests = fields.IntegerField()
    max_guests = fields.IntegerField()
    photo = fields.CharField()
    benefits = fields.EmbeddedDocumentListField(Benefit)
    reservations = fields.EmbeddedDocumentListField(Reservation)
    price_type = fields.IntegerField()
    price_intervals = fields.EmbeddedDocumentListField(PriceInterval)
    general_price = fields.FloatField()
    weekend_increase = fields.IntegerField()

    def calculate_price(self, num_guests, start_date, end_date):
        general_amount = 0
        days = (end_date - start_date).days
        if self.price_type == PriceType.PER_GUEST:
            general_amount += self.general_price * num_guests * days
        else:
            general_amount += self.general_price * days

        weekend_amount = DateRange.get_number_of_weekends(start_date, end_date) * self.weekend_increase
        additional_amount = self.calculate_additional_price(start_date, end_date)

        return general_amount + additional_amount + weekend_amount

    def calculate_additional_price(self, start_date, end_date):
        additional_amount = 0
        valid_price_intervals = []
        for price_interval in self.price_intervals:
            if price_interval.interval.start_date <= end_date and price_interval.interval.end_date >= start_date:
                valid_price_intervals.append(price_interval)
        for interval in valid_price_intervals:
            interval_start = max(interval.interval.start_date, start_date)
            interval_end = min(interval.interval.end_date, end_date)
            interval_days = (interval_end - interval_start).days
            if self.price_type == PriceType.PER_GUEST:
                additional_amount += interval.amount * num_guests * interval_days
            else:
                additional_amount += interval.amount * interval_days
        return additional_amount

    class Meta:
        connection_alias = 'search_db'
        collection_name = 'accommodations'
