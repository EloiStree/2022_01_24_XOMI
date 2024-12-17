import socket
import struct
import time

import socket
import threading


LISTEN_TEXT_IPV4 = "0.0.0.0"
LISTEN_TEXT_PORT = 2506


# CHANGE BY YOUR SERVER IP YOU WANT TO TARGET

## THE HOME SERVER of ELOI for APINT IO
SERVER_UDP_IPV4 = "apint.ddns.net"

## YOUR OWN COMPUTER
# SERVER_UDP_IPV4 = "127.0.0.1"

SERVER_UDP_IPV4 = "192.168.1.42"

# THE HOME SERVER PORT used by Eloi for APINT Unauthentify UDP message when enabled.
SERVER_UDP_PORT = 3615

def listen_udp_messages():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind((LISTEN_TEXT_IPV4, LISTEN_TEXT_PORT))
    while True:
        data, addr = sock.recvfrom(1024)
        text = data.decode()
        push_text_to_interpretor(text)

listener_thread = threading.Thread(target=listen_udp_messages)
listener_thread.daemon = True
listener_thread.start()




def send_udp_integer(integer):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    message = struct.pack("<i", integer)
    sock.sendto(message, (SERVER_UDP_IPV4,SERVER_UDP_PORT))
    sock.close()
    print("Sent: ", integer)


def send_udp_index_integer( index, integer):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    message = struct.pack("<ii", index, integer)
    sock.sendto(message, (SERVER_UDP_IPV4,SERVER_UDP_PORT))
    sock.close()
    print("Sent: ", index, integer)
    
def i(int_value):
    send_udp_integer(int_value)

def ii(index, int_value):
    send_udp_index_integer(index, int_value)


def ii_pr(index, int_value):
    ii(index, int_value)
    ii(index, int_value+1000)
    
def i_pr(int_value):
    i(int_value)
    i(int_value+1000)

def i_p(value):
    i(value)
def i_r(value):
    i(value+1000)
    
def ii_p(index, value):
    ii(index, value)
def ii_r(index, value):
    ii(index, value+1000)
    
step_time = 0.5
def push_all_debug(text, press_integer):
    
    print("Release:", text)
    i(press_integer)
    time.sleep(step_time)
    print("Release:", text)
    i(press_integer+1000)
    time.sleep(step_time)


 
class StringLinkToInteger:
    def __init__(self, string_id, integer_id):
        self.string_id = string_id
        self.integer_id = integer_id
        
  
class XboxIntegerAction:
    
            random_input =  1399
            press_a =  1300
            press_x =  1301
            press_b =  1302
            press_y =  1303
            press_left_side_button =  1304
            press_right_side_button =  1305
            press_left_stick =  1306
            press_right_stick =  1307
            press_menu_right =  1308
            press_menu_left =  1309
            release_dpad =  1310
            press_arrow_north =  1311
            press_arrow_northeast =  1312
            press_arrow_east =  1313
            press_arrow_southeast =  1314
            press_arrow_south =  1315
            press_arrow_southwest =  1316
            press_arrow_west =  1317
            press_arrow_northwest =  1318
            press_xbox_home_button =  1319
            random_axis =  1320
            start_recording =  1321
            set_left_stick_neutral =  1330
            set_left_stick_up =  1331
            set_left_stick_up_right =  1332
            set_left_stick_right =  1333
            set_left_stick_down_right =  1334
            set_left_stick_down =  1335
            set_left_stick_down_left =  1336
            set_left_stick_left =  1337
            set_left_stick_up_left =  1338
            set_right_stick_neutral =  1340
            set_right_stick_up =  1341
            set_right_stick_up_right =  1342
            set_right_stick_right =  1343
            set_right_stick_down_right =  1344
            set_right_stick_down =  1345
            set_right_stick_down_left =  1346
            set_right_stick_left =  1347
            set_right_stick_up_left =  1348
            set_left_stick_horizontal_100 =  1350
            set_left_stick_horizontal_neg_100 =  1351
            set_left_stick_vertical_100 =  1352
            set_left_stick_vertical_neg_100 =  1353
            set_right_stick_horizontal_100 =  1354
            set_right_stick_horizontal_neg_100 =  1355
            set_right_stick_vertical_100 =  1356
            set_right_stick_vertical_neg_100 =  1357
            set_left_trigger_100 =  1358
            set_right_trigger_100 =  1359
            set_left_stick_horizontal_075 =  1360
            set_left_stick_horizontal_neg_075 =  1361
            set_left_stick_vertical_075 =  1362
            set_left_stick_vertical_neg_075 =  1363
            set_right_stick_horizontal_075 =  1364
            set_right_stick_horizontal_neg_075 =  1365
            set_right_stick_vertical_075 =  1366
            set_right_stick_vertical_neg_075 =  1367
            set_left_trigger_075 =  1368
            set_right_trigger_075 =  1369
            set_left_stick_horizontal_050 =  1370
            set_left_stick_horizontal_neg_050 =  1371
            set_left_stick_vertical_050 =  1372
            set_left_stick_vertical_neg_050 =  1373
            set_right_stick_horizontal_050 =  1374
            set_right_stick_horizontal_neg_050 =  1375
            set_right_stick_vertical_050 =  1376
            set_right_stick_vertical_neg_050 =  1377
            set_left_trigger_050 =  1378
            set_right_trigger_050 =  1379
            set_left_stick_horizontal_025 =  1380
            set_left_stick_horizontal_neg_025 =  1381
            set_left_stick_vertical_025 =  1382
            set_left_stick_vertical_neg_025 =  1383
            set_right_stick_horizontal_025 =  1384
            set_right_stick_horizontal_neg_025 =  1385
            set_right_stick_vertical_025 =  1386
            set_right_stick_vertical_neg_025 =  1387
            set_left_trigger_025 =  1388
            set_right_trigger_025 =  1389
            
            
            # Callable by text (duplicate it and make your own version)
            dictionary = {}
            dictionary["random_input"] =  random_input
            dictionary["press_a"] =  press_a
            dictionary["press_x"] =  press_x
            dictionary["press_b"] =  press_b
            dictionary["press_y"] =  press_y
            dictionary["press_left_side_button"] =  press_left_side_button
            dictionary["press_right_side_button"] =  press_right_side_button
            dictionary["press_left_stick"] =  press_left_stick
            dictionary["press_right_stick"] =  press_right_stick
            dictionary["press_menu_right"] =  press_menu_right
            dictionary["press_menu_left"] =  press_menu_left
            dictionary["release_dpad"] =  release_dpad
            dictionary["press_arrow_north"] =  press_arrow_north
            dictionary["press_arrow_northeast"] =  press_arrow_northeast
            dictionary["press_arrow_east"] =  press_arrow_east
            dictionary["press_arrow_southeast"] =  press_arrow_southeast
            dictionary["press_arrow_south"] =  press_arrow_south
            dictionary["press_arrow_southwest"] =  press_arrow_southwest
            dictionary["press_arrow_west"] =  press_arrow_west
            dictionary["press_arrow_northwest"] =  press_arrow_northwest
            dictionary["press_xbox_home_button"] =  press_xbox_home_button
            dictionary["random_axis"] =  random_axis
            dictionary["start_recording"] =  start_recording
            dictionary["set_left_stick_neutral"] =  set_left_stick_neutral
            dictionary["set_left_stick_up"] =  set_left_stick_up
            dictionary["set_left_stick_up_right"] =  set_left_stick_up_right
            dictionary["set_left_stick_right"] =  set_left_stick_right
            dictionary["set_left_stick_down_right"] =  set_left_stick_down_right
            dictionary["set_left_stick_down"] =  set_left_stick_down
            dictionary["set_left_stick_down_left"] =  set_left_stick_down_left
            dictionary["set_left_stick_left"] =  set_left_stick_left
            dictionary["set_left_stick_up_left"] =  set_left_stick_up_left
            dictionary["set_right_stick_neutral"] =  set_right_stick_neutral
            dictionary["set_right_stick_up"] =  set_right_stick_up
            dictionary["set_right_stick_up_right"] =  set_right_stick_up_right
            dictionary["set_right_stick_right"] =  set_right_stick_right
            dictionary["set_right_stick_down_right"] =  set_right_stick_down_right
            dictionary["set_right_stick_down"] =  set_right_stick_down
            dictionary["set_right_stick_down_left"] =  set_right_stick_down_left
            dictionary["set_right_stick_left"] =  set_right_stick_left
            dictionary["set_right_stick_up_left"] =  set_right_stick_up_left
            dictionary["set_left_stick_horizontal_100"] =  set_left_stick_horizontal_100
            dictionary["set_left_stick_horizontal_neg_100"] =  set_left_stick_horizontal_neg_100
            dictionary["set_left_stick_vertical_100"] =  set_left_stick_vertical_100
            dictionary["set_left_stick_vertical_neg_100"] =  set_left_stick_vertical_neg_100
            dictionary["set_right_stick_horizontal_100"] =  set_right_stick_horizontal_100
            dictionary["set_right_stick_horizontal_neg_100"] =  set_right_stick_horizontal_neg_100
            dictionary["set_right_stick_vertical_100"] =  set_right_stick_vertical_100
            dictionary["set_right_stick_vertical_neg_100"] =  set_right_stick_vertical_neg_100
            dictionary["set_left_trigger_100"] =  set_left_trigger_100
            dictionary["set_right_trigger_100"] =  set_right_trigger_100
            dictionary["set_left_stick_horizontal_075"] =  set_left_stick_horizontal_075
            dictionary["set_left_stick_horizontal_neg_075"] =  set_left_stick_horizontal_neg_075
            dictionary["set_left_stick_vertical_075"] =  set_left_stick_vertical_075
            dictionary["set_left_stick_vertical_neg_075"] =  set_left_stick_vertical_neg_075
            dictionary["set_right_stick_horizontal_075"] =  set_right_stick_horizontal_075
            dictionary["set_right_stick_horizontal_neg_075"] =  set_right_stick_horizontal_neg_075
            dictionary["set_right_stick_vertical_075"] =  set_right_stick_vertical_075
            dictionary["set_right_stick_vertical_neg_075"] =  set_right_stick_vertical_neg_075
            dictionary["set_left_trigger_075"] =  set_left_trigger_075
            dictionary["set_right_trigger_075"] =  set_right_trigger_075
            dictionary["set_left_stick_horizontal_050"] =  set_left_stick_horizontal_050
            dictionary["set_left_stick_horizontal_neg_050"] =  set_left_stick_horizontal_neg_050
            dictionary["set_left_stick_vertical_050"] =  set_left_stick_vertical_050
            dictionary["set_left_stick_vertical_neg_050"] =  set_left_stick_vertical_neg_050
            dictionary["set_right_stick_horizontal_050"] =  set_right_stick_horizontal_050
            dictionary["set_right_stick_horizontal_neg_050"] =  set_right_stick_horizontal_neg_050
            dictionary["set_right_stick_vertical_050"] =  set_right_stick_vertical_050
            dictionary["set_right_stick_vertical_neg_050"] =  set_right_stick_vertical_neg_050
            dictionary["set_left_trigger_050"] =  set_left_trigger_050
            dictionary["set_right_trigger_050"] =  set_right_trigger_050
            dictionary["set_left_stick_horizontal_025"] =  set_left_stick_horizontal_025
            dictionary["set_left_stick_horizontal_neg_025"] =  set_left_stick_horizontal_neg_025
            dictionary["set_left_stick_vertical_025"] =  set_left_stick_vertical_025
            dictionary["set_left_stick_vertical_neg_025"] =  set_left_stick_vertical_neg_025
            dictionary["set_right_stick_horizontal_025"] =  set_right_stick_horizontal_025
            dictionary["set_right_stick_horizontal_neg_025"] =  set_right_stick_horizontal_neg_025
            dictionary["set_right_stick_vertical_025"] =  set_right_stick_vertical_025
            dictionary["set_right_stick_vertical_neg_025"] =  set_right_stick_vertical_neg_025
            
            dictionary["r"] =  random_input
            dictionary["a"] =  press_a
            dictionary["x"] =  press_x
            dictionary["b"] =  press_b
            dictionary["y"] =  press_y
            dictionary["slb"] =  press_left_side_button
            dictionary["srb"] =  press_right_side_button
            dictionary["ls"] =  press_left_stick
            dictionary["rs"] =  press_right_stick
            dictionary["mr"] =  press_menu_right
            dictionary["ml"] =  press_menu_left
            dictionary["ac"] =  release_dpad
            dictionary["an"] =  press_arrow_north
            dictionary["ane"] =  press_arrow_northeast
            dictionary["ae"] =  press_arrow_east
            dictionary["ase"] =  press_arrow_southeast
            dictionary["as"] =  press_arrow_south
            dictionary["asw"] =  press_arrow_southwest
            dictionary["aw"] =  press_arrow_west
            dictionary["anw"] =  press_arrow_northwest
            dictionary["home"] =  press_xbox_home_button
            dictionary["rx"] =  random_axis
            
            dictionary["jlc"] =  set_left_stick_neutral
            dictionary["jln"] =  set_left_stick_up
            dictionary["jlne"] =  set_left_stick_up_right
            dictionary["jle"] =  set_left_stick_right
            dictionary["jlse"] =  set_left_stick_down_right
            dictionary["jls"] =  set_left_stick_down
            dictionary["jlsw"] =  set_left_stick_down_left
            dictionary["jlw"] =  set_left_stick_left
            dictionary["jlnw"] =  set_left_stick_up_left
            dictionary["jrc"] =  set_right_stick_neutral
            dictionary["jrn"] =  set_right_stick_up
            dictionary["jrne"] =  set_right_stick_up_right
            dictionary["jre"] =  set_right_stick_right
            dictionary["jrse"] =  set_right_stick_down_right
            dictionary["jrs"] =  set_right_stick_down
            dictionary["jrsx"] =  set_right_stick_down_left
            dictionary["jrw"] =  set_right_stick_left
            dictionary["jrnw"] =  set_right_stick_up_left
            
            
            dictionary["jlh+100"]=set_left_stick_horizontal_100
            dictionary["jlh-100"]=set_left_stick_horizontal_neg_100
            dictionary["jlv+100"]=set_left_stick_vertical_100
            dictionary["jlv-100"]=set_left_stick_vertical_neg_100
            dictionary["jrh+100"]=set_right_stick_horizontal_100
            dictionary["jrh-100"]=set_right_stick_horizontal_neg_100
            dictionary["jrv+100"]=set_right_stick_vertical_100
            dictionary["jrv-100"]=set_right_stick_vertical_neg_100
            dictionary["lt+100"]=set_left_trigger_100
            dictionary["rt+100"]=set_right_trigger_100
            dictionary["jlh+075"]=set_left_stick_horizontal_075
            dictionary["jlh-075"]=set_left_stick_horizontal_neg_075
            dictionary["jlv+075"]=set_left_stick_vertical_075
            dictionary["jlv-075"]=set_left_stick_vertical_neg_075
            dictionary["jrh+075"]=set_right_stick_horizontal_075
            dictionary["jrh-075"]=set_right_stick_horizontal_neg_075
            dictionary["jrv+075"]=set_right_stick_vertical_075
            dictionary["jrv-075"]=set_right_stick_vertical_neg_075
            dictionary["lt+075"]=set_left_trigger_075
            dictionary["rt+075"]=set_right_trigger_075
            dictionary["jlh+050"]=set_left_stick_horizontal_050
            dictionary["jlh-050"]=set_left_stick_horizontal_neg_050
            dictionary["jlv+050"]=set_left_stick_vertical_050
            dictionary["jlv-050"]=set_left_stick_vertical_neg_050
            dictionary["jrh+050"]=set_right_stick_horizontal_050
            dictionary["jrh-050"]=set_right_stick_horizontal_neg_050
            dictionary["jrv+050"]=set_right_stick_vertical_050
            dictionary["jrv-050"]=set_right_stick_vertical_neg_050
            dictionary["lt+050"]=set_left_trigger_050
            dictionary["rt+050"]=set_right_trigger_050
            dictionary["jlh+025"]=set_left_stick_horizontal_025
            dictionary["jlh-025"]=set_left_stick_horizontal_neg_025
            dictionary["jlv+025"]=set_left_stick_vertical_025
            dictionary["jlv-025"]=set_left_stick_vertical_neg_025
            dictionary["jrh+025"]=set_right_stick_horizontal_025
            dictionary["jrh-025"]=set_right_stick_horizontal_neg_025
            dictionary["jrv+025"]=set_right_stick_vertical_025
            dictionary["jrv-025"]=set_right_stick_vertical_neg_025
            dictionary["lt+025"]=set_left_trigger_025
            dictionary["rt+025"]=set_right_trigger_025
            
            dictionary["r1"] =press_right_side_button
            dictionary["r2"] =set_right_trigger_100
            dictionary["l1"] =press_left_side_button
            dictionary["l2"] =set_left_trigger_100
            
            dictionary["al"]=press_arrow_west
            dictionary["ar"]=press_arrow_east
            dictionary["au"]=press_arrow_north
            dictionary["ad"]=press_arrow_south
            dictionary["ac"]=release_dpad
            
            dictionary["br"]= press_b
            dictionary["bd"]= press_a
            dictionary["bl"]= press_x
            dictionary["bu"]= press_y
            
            dictionary["jlr"]= set_left_stick_right 
            dictionary["jll"]= set_left_stick_left
            dictionary["jlu"]= set_left_stick_up
            dictionary["jld"]= set_left_stick_down
            
            dictionary["jrr"]= set_right_stick_right
            dictionary["jrl"]= set_right_stick_left
            dictionary["jru"]= set_right_stick_up
            dictionary["jrd"]= set_right_stick_down
            
            dictionary["jr"]= press_right_stick
            dictionary["jl"]= press_left_stick
            
            dictionary["tl"]= set_left_trigger_100
            dictionary["tr"]= set_right_trigger_100
            dictionary["lt"]= set_left_trigger_100
            dictionary["rt"]= set_right_trigger_100
            
            
def select_character():
    # Validate character selection
    push_all_debug("A", XboxIntegerAction.press_a)
    time.sleep(2)
    push_all_debug("A", XboxIntegerAction.press_a)
    time.sleep(2)
    ii_pr(1, XboxIntegerAction.press_a)
    ii_pr(2, XboxIntegerAction.press_arrow_north)
    ii_pr(2, XboxIntegerAction.press_a)
            
            

def push_text_to_interpretor(text):
        text =text.lower()
        tokens = text.split(" ")
        
        for t in tokens:
            
            try:
                int_value = int(t)
                print("Found: ", int_value)
                i(int_value)
                continue
            except ValueError:
                pass
            
            bool_p1 = t.find("_1")>-1
            bool_p2 = t.find("_2")>-1
            bool_p3 = t.find("_3")>-1
            bool_p4 = t.find("_4")>-1
            bool_all = not bool_p1 and not bool_p2 and not bool_p3 and not bool_p4
            bool_pressed = t.find("+")>-1 or t.find("*")>-1
            bool_release = t.find("-")>-1 or t.find("*")>-1
    
            t= t.replace("+", "")
            t= t.replace("-", "")
            t= t.replace("*", "")
            if bool_p1:
                t = t.replace("_1", "")
            if bool_p2:
                t = t.replace("_2", "")
            if bool_p3:
                t = t.replace("_3", "")
            if bool_p4:
                t = t.replace("_4", "")
            
            
            if not bool_pressed and not bool_release:
                bool_pressed = True
                bool_release = True
                
            
            t = XboxIntegerAction.dictionary.get(t, None)
            if t is not None:    
                print("Found: ", t)
                
                if bool_pressed:
                    if bool_all:
                        i(t)
                    else :
                        if bool_p1:
                            ii(1, t)
                        if bool_p2:
                            ii(2, t)
                        if bool_p3:
                            ii(3, t)
                        if bool_p4:
                            ii(4, t)
                if bool_release:
                    if bool_all:
                        i(t+1000)
                    else :
                        if bool_p1:
                            ii(1, t+1000)
                        if bool_p2:
                            ii(2, t+1000)
                        if bool_p3:
                            ii(3, t+1000)
                        if bool_p4:
                            ii(4, t+1000)
                            
if __name__ == "__main__":
    
    
    # select_character()
    
    
    while True:
        text = input("Enter text: ")
        push_text_to_interpretor(text)
        
                    
    
    bool_loop = False
    if bool_loop:
        while True:
            a =1
            #push_all_debug("Xbox Home Button", 1319)
            push_all_debug("Left Stick", XboxIntegerAction.press_left_stick)
            push_all_debug("Right Stick", XboxIntegerAction.press_right_stick)
            push_all_debug("Menu Right", XboxIntegerAction.press_menu_right)
            push_all_debug("Menu Left", XboxIntegerAction.press_menu_left)
            
            
            push_all_debug("A", XboxIntegerAction.press_a)
            push_all_debug("X", XboxIntegerAction.press_x)
            push_all_debug("B", XboxIntegerAction.press_b)
            push_all_debug("Y", XboxIntegerAction.press_y)
            push_all_debug("Left Side Button", XboxIntegerAction.press_left_side_button)
            push_all_debug("Right Side Button", XboxIntegerAction.press_right_side_button)
            
            push_all_debug("Release Dpad", XboxIntegerAction.release_dpad)
            push_all_debug("Arrow North", XboxIntegerAction.press_arrow_north)
            push_all_debug("Arrow Northeast", XboxIntegerAction.press_arrow_northeast)
            push_all_debug("Arrow East", XboxIntegerAction.press_arrow_east)
            push_all_debug("Arrow Southeast", XboxIntegerAction.press_arrow_southeast)
            push_all_debug("Arrow South", XboxIntegerAction.press_arrow_south)
            push_all_debug("Arrow Southwest", XboxIntegerAction.press_arrow_southwest)
            push_all_debug("Arrow West", XboxIntegerAction.press_arrow_west)
            push_all_debug("Arrow Northwest", XboxIntegerAction.press_arrow_northwest)
            push_all_debug("Arrow North", XboxIntegerAction.press_arrow_north)
            push_all_debug("Release Dpad", XboxIntegerAction.release_dpad)
            push_all_debug("Trigger Left", XboxIntegerAction.set_left_trigger_100)
            push_all_debug("Trigger Right", XboxIntegerAction.set_right_trigger_100)
            
            # trigger 1 0.75 0.5 0.25 0
            push_all_debug("Trigger Left 0.75", XboxIntegerAction.set_left_trigger_075)
            push_all_debug("Trigger Right 0.75", XboxIntegerAction.set_right_trigger_075)
            push_all_debug("Trigger Left 0.5", XboxIntegerAction.set_left_trigger_050)
            push_all_debug("Trigger Right 0.5", XboxIntegerAction.set_right_trigger_050)
            push_all_debug("Trigger Left 0.25", XboxIntegerAction.set_left_trigger_025)
            push_all_debug("Trigger Right 0.25", XboxIntegerAction.set_right_trigger_025)
            
            # Left joystick
            push_all_debug("Left Stick Neutral", XboxIntegerAction.set_left_stick_neutral)
            push_all_debug("Left Stick Up", XboxIntegerAction.set_left_stick_up)
            push_all_debug("Left Stick Up Right", XboxIntegerAction.set_left_stick_up_right)
            push_all_debug("Left Stick Right", XboxIntegerAction.set_left_stick_right)
            push_all_debug("Left Stick Down Right", XboxIntegerAction.set_left_stick_down_right)
            push_all_debug("Left Stick Down", XboxIntegerAction.set_left_stick_down)
            push_all_debug("Left Stick Down Left", XboxIntegerAction.set_left_stick_down_left)
            push_all_debug("Left Stick Left", XboxIntegerAction.set_left_stick_left)
            push_all_debug("Left Stick Up Left", XboxIntegerAction.set_left_stick_up_left)
            push_all_debug("Left Stick Up", XboxIntegerAction.set_left_stick_up)
            
            # Right joystick
            push_all_debug("Right Stick Neutral", XboxIntegerAction.set_right_stick_neutral)
            push_all_debug("Right Stick Up", XboxIntegerAction.set_right_stick_up)
            push_all_debug("Right Stick Up Right", XboxIntegerAction.set_right_stick_up_right)
            push_all_debug("Right Stick Right", XboxIntegerAction.set_right_stick_right)
            push_all_debug("Right Stick Down Right", XboxIntegerAction.set_right_stick_down_right)
            push_all_debug("Right Stick Down", XboxIntegerAction.set_right_stick_down)
            push_all_debug("Right Stick Down Left", XboxIntegerAction.set_right_stick_down_left)
            push_all_debug("Right Stick Left", XboxIntegerAction.set_right_stick_left)
            push_all_debug("Right Stick Up Left", XboxIntegerAction.set_right_stick_up_left)
            push_all_debug("Right Stick Up", XboxIntegerAction.set_right_stick_up)
            
            
            # Left stick 1 0.75 0.5 0.25 0
            push_all_debug("Left Stick Horizontal 1", XboxIntegerAction.set_left_stick_horizontal_100)
            push_all_debug("Left Stick Horizontal -1", XboxIntegerAction.set_left_stick_horizontal_neg_100)
            push_all_debug("Left Stick Vertical 1", XboxIntegerAction.set_left_stick_vertical_100)
            push_all_debug("Left Stick Vertical -1", XboxIntegerAction.set_left_stick_vertical_neg_100)
            push_all_debug("Left Stick Horizontal 0.75", XboxIntegerAction.set_left_stick_horizontal_075)
            push_all_debug("Left Stick Horizontal -0.75", XboxIntegerAction.set_left_stick_horizontal_neg_075)
            push_all_debug("Left Stick Vertical 0.75", XboxIntegerAction.set_left_stick_vertical_075)
            push_all_debug("Left Stick Vertical -0.75", XboxIntegerAction.set_left_stick_vertical_neg_075)
            push_all_debug("Left Stick Horizontal 0.5", XboxIntegerAction.set_left_stick_horizontal_050)
            push_all_debug("Left Stick Horizontal -0.5", XboxIntegerAction.set_left_stick_horizontal_neg_050)
            push_all_debug("Left Stick Vertical 0.5", XboxIntegerAction.set_left_stick_vertical_050)
            push_all_debug("Left Stick Vertical -0.5", XboxIntegerAction.set_left_stick_vertical_neg_050)
            push_all_debug("Left Stick Horizontal 0.25", XboxIntegerAction.set_left_stick_horizontal_025)
            push_all_debug("Left Stick Horizontal -0.25", XboxIntegerAction.set_left_stick_horizontal_neg_025)
            push_all_debug("Left Stick Vertical 0.25", XboxIntegerAction.set_left_stick_vertical_025)
            push_all_debug("Left Stick Vertical -0.25", XboxIntegerAction.  set_left_stick_vertical_neg_025)
            
            
            # Right stick 1 0.75 0.5 0.25 0
            push_all_debug("Right Stick Horizontal 1", XboxIntegerAction.set_right_stick_horizontal_100)
            push_all_debug("Right Stick Horizontal -1", XboxIntegerAction.set_right_stick_horizontal_neg_100)
            push_all_debug("Right Stick Vertical 1", XboxIntegerAction.set_right_stick_vertical_100)
            push_all_debug("Right Stick Vertical -1", XboxIntegerAction.set_right_stick_vertical_neg_100)
            push_all_debug("Right Stick Horizontal 0.75", XboxIntegerAction.set_right_stick_horizontal_075)
            push_all_debug("Right Stick Horizontal -0.75", XboxIntegerAction.set_right_stick_horizontal_neg_075)
            push_all_debug("Right Stick Vertical 0.75", XboxIntegerAction.set_right_stick_vertical_075)
            push_all_debug("Right Stick Vertical -0.75", XboxIntegerAction.set_right_stick_vertical_neg_075)
            push_all_debug("Right Stick Horizontal 0.5", XboxIntegerAction.set_right_stick_horizontal_050)
            push_all_debug("Right Stick Horizontal -0.5", XboxIntegerAction.set_right_stick_horizontal_neg_050)
            push_all_debug("Right Stick Vertical 0.5", XboxIntegerAction.set_right_stick_vertical_050)
            push_all_debug("Right Stick Vertical -0.5", XboxIntegerAction.set_right_stick_vertical_neg_050)
            push_all_debug("Right Stick Horizontal 0.25", XboxIntegerAction.set_right_stick_horizontal_025)
            push_all_debug("Right Stick Horizontal -0.25", XboxIntegerAction.set_right_stick_horizontal_neg_025)
            push_all_debug("Right Stick Vertical 0.25", XboxIntegerAction.set_right_stick_vertical_025)
            push_all_debug("Right Stick Vertical -0.25", XboxIntegerAction.set_right_stick_vertical_neg_025)
            
            
            
            
            
            # Push random value then release to see if everything works
            i(int_random_input)
            time.sleep(2)
            ii(1, int_random_input+1000)
            time.sleep(2)
            ii(2, int_random_input)
            time.sleep(2)
            i( int_random_input+1000)
            time.sleep(10)
            
        