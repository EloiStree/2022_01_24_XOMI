# 2022_01_24_XOMI
XOmi is a small tool that listen to UDP message to execute Xbox commands.

> Note :Code is not clean or beautiful at all.
> But is works and that good enough for me for now.



## How to use ?

Send UDP message as ASCII to the port of the app (2504 by default).


Alias to call
jlpn jlpne jlpe jlpse jlps jlpso jlpo jlpno
jrpn jrpne jrpe jrpse jrps jrpso jrpo jrpno 

You can request a random direction from your joystick.
jrrandom
jlrandom
jrrandnorm
jlrandnorm
       
flush  // Will remote all in waiting command to execute
release  // Will release all the button of the controller
stop  // Will remote all waiting then release the button
plug  // Will reconnect the device (*not tested, in the list)
unplug // Will disconnect the deviec (*not tested, in the list)
jlzero jlz //Will reset the joystick left to neutral state
jrzero jrz //Will reset the joystick right to neutral state


You can set the joystick state with a command joysticktype % horizontalpercent % verticalpercent
jr%0.1%0.2 jl%0.1%0.2
jl left 
jr right 


  
jlh%0.1 jlv%0.1  jrh%0.1 jrv%0.1
 tr   rt 
 tl   lt 
 jlv   lv 
 jlh   lh 
 jrv   rv 
 jrh   rh 

 tl    lt    l2    TriggerLeft 
 tr    rt    r2    TriggerRight 
 sbl    l1    lb    SideButtonLeft 
 sbr    r1    rb    SideButtonRight 
 al    ArrowLeft 
 ar    ArrowRight 
 ad    ArrowDown    ab 
 au    ArrowUp    at 
 a    ba    bd    paddown    pd    buttondown 
 b    bb    br    padright    pr    ButtonRigh 
 ft    x    bx    bl    padleft    pl    ButtonLeft 
 y    by    bu    padup    pu    ButtonUp 
 m    ml    menu    b    back    MenuLeft 
 s    mr    start    buttoMenuRightndown 
 jl    jlb    JoystickLeftButton 
 jr    jrb    JoystickRightButton 
 jlr    jle    JoystickLeftRight 
 jlu    jln    JoystickLeftUp 
 jld    jls    JoystickLeftDown 
 jll    jlw    JoystickLeftLeft 
 jrr    jre    JoystickRightRight 
 jru    jrn    JoystickRightUp 
 jrd    jrs    JoystickRightDown 
 jrl    jrw    JoystickRightLeft 
 mc    guide    xbox    XboxMenu 
 jlr    jle    JoystickLeftRight 



