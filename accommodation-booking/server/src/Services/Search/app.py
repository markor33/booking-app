import os
import threading
import uuid
import json
import grpc
import logging
from flask import Flask, request, jsonify
from dotenv import load_dotenv
from pymodm import connect
from datetime import datetime, date
from models.models import *
from services.search import search_accommodations
from concurrent import futures
from dto import *
import grpc_services

load_dotenv()
connect(os.environ.get('MONGODB_URI'), alias='search_db')
app = Flask(__name__)

@app.route("/api/accommodation/search", methods=['POST'])
def search():
    data = request.json
    location = data.get('location')
    num_guests = data.get('numGuests')
    start_date = datetime.fromisoformat(data.get('startDate').replace('Z', ''))
    end_date = datetime.fromisoformat(data.get('endDate').replace('Z', ''))

    accommodation_list = search_accommodations(location, num_guests, start_date, end_date)
    return jsonify(AccommodationDTO.list_to_DTO(accommodation_list, num_guests, start_date, end_date))

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    grpc_services.accommodation_search_pb2_grpc.add_AccommodationSearchServicer_to_server(grpc_services.accommodation_service.AccommodationService(), server)
    server.add_insecure_port('[::]:5001')
    server.start()
    server.wait_for_termination()

if __name__ == "__main__":
    logging.basicConfig(level=logging.DEBUG)
    serve_thread = threading.Thread(target=serve)
    serve_thread.start()
    app.run(host='0.0.0.0', debug=True)
