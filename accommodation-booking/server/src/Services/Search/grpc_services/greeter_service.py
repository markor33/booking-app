from . import search_pb2
from . import search_pb2_grpc

class GreeterServicer(search_pb2_grpc.GreeterServicer):
    def SayHello(self, request, context):
        response = search_pb2.HelloResponse()
        response.message = 'Hello, %s!' % request.name
        return response
        
