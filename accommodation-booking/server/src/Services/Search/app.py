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
from datetime import datetime, date
from models.models import *
from services.search import search_accommodations
from  concurrent import futures

load_dotenv()
connect(os.environ.get('MONGODB_URI'), alias='search_db')
app = Flask(__name__)

@app.route("/accommodation/search", methods=['POST'])
def search():
    data = request.json
    country = data.get('location')['country']
    city = data.get('location')['city']
    num_guests = data.get('num_guests')
    start_date = datetime.strptime(data.get('start_date'), '%Y-%m-%d')
    end_date = datetime.strptime(data.get('end_date'), '%Y-%m-%d')

    accommodation_list = search_accommodations(country, city, num_guests, start_date, end_date)
    return jsonify(accommodation_list)

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    search_pb2_grpc.add_GreeterServicer_to_server(greeter_service.GreeterServicer(), server)
    server.add_insecure_port('[::]:5001')
    server.start()
    server.wait_for_termination()

if __name__ == "__main__":
    serve_thread = threading.Thread(target=serve)
    serve_thread.start()
    app.run(host='0.0.0.0', debug=True)
