from playerAPI import *

if __name__ == "__main__":
    open_connection()
    for _ in range(1):
        forward(3)
        turn("right")
        forward()
        turn("right")
        forward(3)
        turn("right")
        forward()
        turn("right")
    close_connection()