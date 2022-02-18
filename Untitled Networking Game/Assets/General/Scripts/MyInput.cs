using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput
{
    static string[] xAxis = {"Player1_Horizontal", "Player2_Horizontal", "Computer1_Horizontal", "Computer_2_Horizontal"};
    static string[] yAxis = {"Player1_Vertical", "Player2_Vertical", "Computer1_Vertical", "Computer2_Vertical"};

    //buttons
    static KeyCode[] interactButton = {KeyCode.Joystick1Button1, KeyCode.Joystick2Button1, KeyCode.Space, KeyCode.KeypadEnter};
    static KeyCode[] backButton = {KeyCode.Joystick1Button2, KeyCode.Joystick2Button2, KeyCode.Escape, KeyCode.Backspace};
    static KeyCode[] pauseButton = {KeyCode.Joystick1Button9, KeyCode.Joystick2Button9, KeyCode.Escape, KeyCode.Backspace};

    //detect if a player pressed the interact/jump button
    public static bool GetKeyInteract(int player)
    {
        return Input.GetKey(interactButton[player]);
    }

    //detect if a player pressed the back/cancel button
    public static bool GetKeyBack(int player)
    {
        return Input.GetKey(backButton[player]);
    }

    //detect if a player pressed the pause button
    public static bool GetKeyPause(int player)
    {
        return Input.GetKey(pauseButton[player]);
    }

    //detect if a player is using the X-axis
    public static float GetXAxis(int player)
    {
        return Input.GetAxis(xAxis[player]);
    }

    //detect if a player is using the Y-axis
    public static float GetYAxis(int player)
    {
        return Input.GetAxis(yAxis[player]);
    }
}
