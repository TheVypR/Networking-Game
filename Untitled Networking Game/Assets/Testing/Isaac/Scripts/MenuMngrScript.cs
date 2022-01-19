using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMngrScript : MonoBehaviour
{
    public void OnPlayPressed()
    {
        //switch to the saved game canvas

    }

    public void OnOptionsPressed()
    {
        //switch to options canvas

    }

    public void OnQuitPressed()
    {
        //close the game
        Application.Quit();
    }//end OnQuitPressed

    public void OptionPressed(string option)
    {
        //change appropriate options

    }//end OptionPressed

    public void SaveChosen(int saveNum)
    {
        //load a saved game based on number chosen

    }
}
