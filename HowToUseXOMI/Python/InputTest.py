import socket
import time
import struct



def send_numbers(numbers, delay):
    with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:
        s.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
        for num in numbers:
            data = struct.pack('<i', num)
            s.sendto(data, ('255.255.255.255', 3615))
            time.sleep(delay)


print("Starting in 5 seconds...")
send_numbers([1390,2390], 0.1)
send_numbers([1330,2330,1340,2340], 0.1)
time.sleep(5)
print ("Starting now!")

while True:
    time.sleep(1)
    send_numbers([1300,2300,1301,2301], 0.1)
    time.sleep(1)
    send_numbers([1337,2337,1333,2333], 0.1)
