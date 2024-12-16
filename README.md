# 2022_01_24_XOMI                                                                                          
XOmi is a small tool that listen to UDP message to execute Xbox commands.  
  
> Note :Code is not clean or beautiful at all.  
> But is works and that good enough for me for now.  
  
  

# V0.1: Integer version

See the mapping of the key here:  
[https://github.com/EloiStree/2024_08_29_ScratchToWarcraft](https://github.com/EloiStree/2024_08_29_ScratchToWarcraft)  



# V 0.0: Text version

**Download:** https://github.com/EloiStree/2022_01_24_XOMI/releases/tag/V0.0

## How to use ?  
  
Send UDP message as ASCII to the port of the app (2504 by default).  
  
  
Alias to call    
`jlpn jlpne jlpe jlpse jlps jlpso jlpo jlpno`    
`jrpn jrpne jrpe jrpse jrps jrpso jrpo jrpno`    
  
You can request a random direction from your joystick.    
`jrrandom` // Will set the joystick right to random position    
`jlrandom` // Will set the joystick left to random position    
`jrrandnorm` // Will set the joystick right to random position normalized (set to max)  
`jlrandnorm` // Will set the joystick right to random position normalized (set to max)  
         
`flush` // Will remote all in waiting command to execute  
`release` // Will release all the button of the controller  
`stop` // Will remote all waiting then release the button  
`plug`  // Will reconnect the device (*not tested, in the list)  
`unplug` // Will disconnect the deviec (*not tested, in the list)  
`jlzero jlz` //Will reset the joystick left to neutral state  
`jrzero jrz` //Will reset the joystick right to neutral state  
  
  
You can set the joystick state with a command `joysticktype % horizontalpercent % verticalpercent`  
`jr%0.1%0.2 jl%0.1%0.2`  
`jl left `  
`jr right`   
  
  
You can set the triggers or joystick state from here `axistype %  percent to apply`  
`jlh%0.1 jlv%0.1  jrh%0.1 jrv%0.1`  
 `tr   rt` // Right Trigger  
 `tl   lt`  // Left Trigger  
 `jlv   lv`  // Left vertical Joystick axis  
 `jlh   lh` // Left horizontal Joystick axis  
 `jrv   rv` // Right vertical Joystick axis  
 `jrh   rh` // Right horizontal Joystick axis  
  
  
All the following lable are button. You can use `_-= '. `  
 `tl    lt    l2    TriggerLeft `  
 `tr    rt    r2    TriggerRight `  
 `sbl    l1    lb    SideButtonLeft `  
 `sbr    r1    rb    SideButtonRight `  
 `al    ArrowLeft `  
 `ar    ArrowRight `  
 `ad  ab   ArrowDown    `  
 `au   at   ArrowUp   `  
 `a    ba    bd    pd  paddown  buttondown `                
 `b    bb    br    pr  padright  ButtonRigh `  
 `ft    x    bx    bl    pl  padleft  ButtonLeft `  
 `y    by    bu    pu  padup  ButtonUp `  
 `m    ml    b  menu  back    MenuLeft `  
 `s    mr    start    buttoMenuRightndown `  
 `jl    jlb    JoystickLeftButton `  
 `jr    jrb    JoystickRightButton `  
 `jlr    jle    JoystickLeftRight `  
 `jlu    jln    JoystickLeftUp `  
 `jld    jls    JoystickLeftDown `  
 `jll    jlw    JoystickLeftLeft `  
 `jrr    jre    JoystickRightRight `  
 `jru    jrn    JoystickRightUp `  
 `jrd    jrs    JoystickRightDown `  
 `jrl    jrw    JoystickRightLeft `  
 `mc    guide    xbox    XboxMenu `     
 `jlr    jle    JoystickLeftRight`                                                                      



