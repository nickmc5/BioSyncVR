import socket 
import time

connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
connection.bind('localhost', 12345)
connection.listen(1)

pipe, add = connection.accept()

transmitting = True

while transmitting:
    #grab signal processing data
    pipe.sendall("{#this is where we send in our data as a string}")
    time.sleep(1/60) #to simulate sending 60 hz data

pipe.close()
connection.close()