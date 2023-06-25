import socket
from time import sleep

host, port = "127.0.0.1", 25001
data = "1,0,3"

# SOCK_STREAM means TCP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

def open_connection():
    sock.connect((host, port))

def close_connection():
    sock.close()

def forward(steps: int = 1):
    sock.sendall(f"forward {steps}".encode("utf-8"))
    sleep(0.5)

def turn(direction: str):
    sock.sendall(f"turn {direction}".encode("utf-8"))
    sleep(0.5)