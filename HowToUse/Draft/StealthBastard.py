
import socket
import time

TARGET_PORT = 2506
TARGET_IP = "127.0.0.1"

def push_text_to_port(text):
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.sendto(text.encode(), (TARGET_IP, TARGET_PORT))
    s.close()


# time.sleep(5)
# push_text_to_port("a+")
# time.sleep(1)
# push_text_to_port("a-")
# time.sleep(1)
# push_text_to_port("a+")
# time.sleep(1)
# push_text_to_port("a-")

# push_text_to_port("jll+")
# time.sleep(2)
# push_text_to_port("jll-")
# time.sleep(1)

# time.sleep(1)
# push_text_to_port("x+")
# time.sleep(1)
# push_text_to_port("x-")



def move_left(move_time):
    push_text_to_port("jll+")
    time.sleep(move_time)
    push_text_to_port("jll-")

def move_right(move_time):
    push_text_to_port("jlr+")
    time.sleep(move_time)
    push_text_to_port("jlr-")

def jump_valide(move_time):
    push_text_to_port("a+")
    time.sleep(move_time)
    push_text_to_port("a-")


def x(move_time):
    push_text_to_port("x+")
    time.sleep(move_time)
    push_text_to_port("x-")

def up_arrow(move_time):
    push_text_to_port("ua+")
    time.sleep(move_time)
    push_text_to_port("ua-")

def start_move_right():
    push_text_to_port("jlr+")

def stop_move_right():
    push_text_to_port("jlr-")

def release_all():
    push_text_to_port("2399")

# time.sleep(3)


# ## Step 2 of the level
# jump_valide(0.1)
# move_right(3)
# move_left(4)

# Step 3 of the level

# Jump the first hole
# #while True: 
#     release_all()
#     jump_valide(0.1)
#     time.sleep(3)
#     release_all()
#     time.sleep(1)
#     start_move_right()
#     time.sleep(0.45)
#     jump_valide(1)
#     time.sleep(2)
#     stop_move_right()

# # print("go in the game")
# # time.sleep(3)
# # print("Ready")

# # jump_valide(0.1)
# # release_all()
# # print("Move Left")
# # move_left(0.3)
# # print("valide")
# # up_arrow(0.1)



# time.sleep(3)
# push_text_to_port("a")
# push_text_to_port("a")
# time.sleep(4)

# push_text_to_port("a+")
# time.sleep(0.5)
# push_text_to_port("jlr+")
# time.sleep(1)
# push_text_to_port( "jlr-" )
# push_text_to_port("a-")


while True:

    print("What do you want to do ?")
    console_text = input()
    print(console_text)
   # time.sleep(3)
    split_part = console_text.split(" ")
    for t  in split_part:
        print(t)
        push_text_to_port(t)
        time.sleep(1)

