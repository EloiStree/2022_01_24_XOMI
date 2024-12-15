import socket
import struct
import time

IPV4 = "192.168.1.51"
PORT = 2505

def send_udp_integer(integer):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    message = struct.pack("<i", integer)
    sock.sendto(message, (IPV4,PORT))
    sock.close()
    print("Sent: ", integer)


def send_udp_index_integer( index, integer):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    message = struct.pack("<ii", index, integer)
    
    sock.sendto(message, (IPV4,PORT))
    sock.close()
    print("Sent: ", index, integer)
    
def i(int_value):
    send_udp_integer(int_value)

def ii(index, int_value):
    send_udp_index_integer(index, int_value)


step_time = 0.5
def push_all_debug(text, press_integer):
    
    print("Release:", text)
    i(press_integer)
    time.sleep(step_time)
    print("Release:", text)
    i(press_integer+1000)
    time.sleep(step_time)

if __name__ == "__main__":
    integer = 42 
    int_random_input=1399
    while True:
        
        
        #push_all_debug("Xbox Home Button", 1319)
        push_all_debug("Left Stick", 1306)
        push_all_debug("Right Stick", 1307)
        
        push_all_debug("Menu Right", 1308)
        push_all_debug("Menu Left", 1309)
        
        
        push_all_debug("A", 1300)
        push_all_debug("X", 1301)
        push_all_debug("B", 1302)
        push_all_debug("Y", 1303)
        push_all_debug("Left Side Button", 1304)
        push_all_debug("Right Side Button", 1305)
        
        push_all_debug("Release Dpad", 1310)
        push_all_debug("Arrow North", 1311)
        push_all_debug("Arrow Northeast", 1312)
        push_all_debug("Arrow East", 1313)
        push_all_debug("Arrow Southeast", 1314)
        push_all_debug("Arrow South", 1315)
        push_all_debug("Arrow Southwest", 1316)
        push_all_debug("Arrow West", 1317)
        push_all_debug("Arrow Northwest", 1318)
        push_all_debug("Arrow North", 1311)
        push_all_debug("Release Dpad", 1310)
        push_all_debug("Trigger Left", 1358)
        push_all_debug("Trigger Right", 1359)
        
        # trigger 1 0.75 0.5 0.25 0
        push_all_debug("Trigger Left 0.75", 1368)
        push_all_debug("Trigger Right 0.75", 1369)
        push_all_debug("Trigger Left 0.5", 1378)
        push_all_debug("Trigger Right 0.5", 1379)
        push_all_debug("Trigger Left 0.25", 1388)
        push_all_debug("Trigger Right 0.25", 1389)
        
        # Left joystick
        push_all_debug("Left Stick Neutral", 1330)
        push_all_debug("Left Stick Up", 1331)
        push_all_debug("Left Stick Up Right", 1332)
        push_all_debug("Left Stick Right", 1333)
        push_all_debug("Left Stick Down Right", 1334)
        push_all_debug("Left Stick Down", 1335)
        push_all_debug("Left Stick Down Left", 1336)
        push_all_debug("Left Stick Left", 1337)
        push_all_debug("Left Stick Up Left", 1338)
        push_all_debug("Left Stick Up", 1331)
        
        # Right joystick
        push_all_debug("Right Stick Neutral", 1340)
        push_all_debug("Right Stick Up", 1341)
        push_all_debug("Right Stick Up Right", 1342)
        push_all_debug("Right Stick Right", 1343)
        push_all_debug("Right Stick Down Right", 1344)
        push_all_debug("Right Stick Down", 1345)
        push_all_debug("Right Stick Down Left", 1346)
        push_all_debug("Right Stick Left", 1347)
        push_all_debug("Right Stick Up Left", 1348)
        push_all_debug("Right Stick Up", 1341)
        
        
        # Left stick 1 0.75 0.5 0.25 0
        push_all_debug("Left Stick Horizontal 1", 1350)
        push_all_debug("Left Stick Horizontal -1", 1351)
        push_all_debug("Left Stick Vertical 1", 1352)
        push_all_debug("Left Stick Vertical -1", 1353)
        push_all_debug("Left Stick Horizontal 0.75", 1360)
        push_all_debug("Left Stick Horizontal -0.75", 1361)
        push_all_debug("Left Stick Vertical 0.75", 1362)
        push_all_debug("Left Stick Vertical -0.75", 1363)
        push_all_debug("Left Stick Horizontal 0.5", 1370)
        push_all_debug("Left Stick Horizontal -0.5", 1371)
        push_all_debug("Left Stick Vertical 0.5", 1372)
        push_all_debug("Left Stick Vertical -0.5", 1373)
        push_all_debug("Left Stick Horizontal 0.25", 1380)
        push_all_debug("Left Stick Horizontal -0.25", 1381)
        push_all_debug("Left Stick Vertical 0.25", 1382)
        push_all_debug("Left Stick Vertical -0.25", 1383)
        
        
        # Right stick 1 0.75 0.5 0.25 0
        push_all_debug("Right Stick Horizontal 1", 1354)
        push_all_debug("Right Stick Horizontal -1", 1355)
        push_all_debug("Right Stick Vertical 1", 1356)
        push_all_debug("Right Stick Vertical -1", 1357)
        push_all_debug("Right Stick Horizontal 0.75", 1364)
        push_all_debug("Right Stick Horizontal -0.75", 1365)
        push_all_debug("Right Stick Vertical 0.75", 1366)
        push_all_debug("Right Stick Vertical -0.75", 1367)
        push_all_debug("Right Stick Horizontal 0.5", 1374)
        push_all_debug("Right Stick Horizontal -0.5", 1375)
        push_all_debug("Right Stick Vertical 0.5", 1376)
        push_all_debug("Right Stick Vertical -0.5", 1377)
        push_all_debug("Right Stick Horizontal 0.25", 1384)
        push_all_debug("Right Stick Horizontal -0.25", 1385)
        push_all_debug("Right Stick Vertical 0.25", 1386)
        push_all_debug("Right Stick Vertical -0.25", 1387)
        
        
        
        
        
        
        # i(int_random_input)
        # time.sleep(2)
        # ii(1, int_random_input+1000)
        # time.sleep(2)
        # ii(2, int_random_input)
        # time.sleep(2)
        # i( int_random_input+1000)
        # time.sleep(10)
        
        
class XboxIntegerAction:
    def __init__(self):
            self.random_input =  1399
            self.press_a =  1300
            self.press_x =  1301
            self.press_b =  1302
            self.press_y =  1303
            self.press_left_side_button =  1304
            self.press_right_side_button =  1305
            self.press_left_stick =  1306
            self.press_right_stick =  1307
            self.press_menu_right =  1308
            self.press_menu_left =  1309
            self.release_dpad =  1310
            self.press_arrow_north =  1311
            self.press_arrow_northeast =  1312
            self.press_arrow_east =  1313
            self.press_arrow_southeast =  1314
            self.press_arrow_south =  1315
            self.press_arrow_southwest =  1316
            self.press_arrow_west =  1317
            self.press_arrow_northwest =  1318
            self.press_xbox_home_button =  1319
            self.random_axis =  1320
            self.start_recording =  1321
            self.set_left_stick_neutral =  1330
            self.move_left_stick_up =  1331
            self.move_left_stick_up_right =  1332
            self.move_left_stick_right =  1333
            self.move_left_stick_down_right =  1334
            self.move_left_stick_down =  1335
            self.move_left_stick_down_left =  1336
            self.move_left_stick_left =  1337
            self.move_left_stick_up_left =  1338
            self.set_right_stick_neutral =  1340
            self.move_right_stick_up =  1341
            self.move_right_stick_up_right =  1342
            self.move_right_stick_right =  1343
            self.move_right_stick_down_right =  1344
            self.move_right_stick_down =  1345
            self.move_right_stick_down_left =  1346
            self.move_right_stick_left =  1347
            self.move_right_stick_up_left =  1348
            self.set_left_stick_horizontal_100 =  1350
            self.set_left_stick_horizontal_neg_100 =  1351
            self.set_left_stick_vertical_100 =  1352
            self.set_left_stick_vertical_neg_100 =  1353
            self.set_right_stick_horizontal_100 =  1354
            self.set_right_stick_horizontal_neg_100 =  1355
            self.set_right_stick_vertical_1 =  1356
            self.set_right_stick_vertical_neg_100 =  1357
            self.set_left_trigger_100 =  1358
            self.set_right_trigger_100 =  1359
            self.set_left_stick_horizontal_075 =  1360
            self.set_left_stick_horizontal_neg_075 =  1361
            self.set_left_stick_vertical_075 =  1362
            self.set_left_stick_vertical_neg_075 =  1363
            self.set_right_stick_horizontal_075 =  1364
            self.set_right_stick_horizontal_neg_075 =  1365
            self.set_right_stick_vertical_075 =  1366
            self.set_right_stick_vertical_neg_075 =  1367
            self.set_left_trigger_075 =  1368
            self.set_right_trigger_705 =  1369
            self.set_left_stick_horizontal_050 =  1370
            self.set_left_stick_horizontal_neg_050 =  1371
            self.set_left_stick_vertical_050 =  1372
            self.set_left_stick_vertical_neg_050 =  1373
            self.set_right_stick_horizontal_050 =  1374
            self.set_right_stick_horizontal_neg_050 =  1375
            self.set_right_stick_vertical_050 =  1376
            self.set_right_stick_vertical_neg_050 =  1377
            self.set_left_trigger_050 =  1378
            self.set_right_trigger_050 =  1379
            self.set_left_stick_horizontal_025 =  1380
            self.set_left_stick_horizontal_neg_025 =  1381
            self.set_left_stick_vertical_025 =  1382
            self.set_left_stick_vertical_neg_025 =  1383
            self.set_right_stick_horizontal_025 =  1384
            self.set_right_stick_horizontal_neg_025 =  1385
            self.set_right_stick_vertical_025 =  1386
            self.set_right_stick_vertical_neg_025 =  1387
            self.set_left_trigger_25 =  1388
            self.set_right_trigger_25 =  1389
        

    def get_action_code(self, action_name):
        """Retrieve the integer code for the given action name."""
        return self.actions.get(action_name, None)

    def perform_action(self, action_name):
        """Simulate performing the action by printing its code."""
        action_code = self.get_action_code(action_name)
        if action_code:
            print(f"Performing action: {action_name} with code {action_code}")
        else:
            print(f"Action '{action_name}' not found!")