Controls for running PC build:
PS4 Button  | PC Equivalent
	X	 |		H
   Square	 |		J
   Circle   |		K
   Triangle	 |		L

 P2 joystick| Arrow Keys
 P1 joystick| WASD
 P1 jump	 | Spacebar

  Menu Nav	 | Use Mouse

Note: the provided build is designed for PC use, it builds to PS4 (and we fixed the start game bug from the demo) but we didn't include that build. If you would like to build to PS4, find the MyInput calls in PlayerMovement.cs and change the parameter number from 2 -> 0 and in Player2.cs change the number (except in the PS4Button calls) from 1 -> 3.