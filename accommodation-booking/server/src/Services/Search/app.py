import os
import threading
import uuid
import json
import grpc
import grpc_services.search_pb2
import grpc_services.search_pb2_grpc
from flask import Flask, request, jsonify
from grpc_services import greeter_service
from grpc_services import search_pb2, search_pb2_grpc
from dotenv import load_dotenv
from pymodm import connect
from datetime import datetime
from models.models import *
from  concurrent import futures

load_dotenv()
connect(os.environ.get('MONGODB_URI'), alias='search_db')
app = Flask(__name__)

@app.route("/accommodation/search", methods=['POST'])
def home():
    data = request.json
    start_date = datetime.strptime(data.get('start_date'), '%Y-%m-%d')
    end_date = datetime.strptime(data.get('end_date'), '%Y-%m-%d')

    accommodations = Accommodation.objects.raw({
        '$or': [
            { 'location.country': data.get('location')['country'] },
            { 'location.city': data.get('location')['city'] }
        ],
        'minGuests': {'$lte': data.get('num_guests')},
        'maxGuests': {'$gte': data.get('num_guests')},
        '$nor': [
            {
                '$and': [
                    {'reservations.period.start_date': {'$lte': end_date}},
                    {'reservations.period.end_date': {'$gte': start_date}}
                ],
            }
        ]
    }).all()

    accommodation_list = [accommodation.to_son().to_dict() for accommodation in accommodations]
    return jsonify(accommodation_list)

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    search_pb2_grpc.add_GreeterServicer_to_server(greeter_service.GreeterServicer(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    server.wait_for_termination()

if __name__ == "__main__":
    serve_thread = threading.Thread(target=serve)
    serve_thread.start()
    app.run(host='0.0.0.0', debug=True)
