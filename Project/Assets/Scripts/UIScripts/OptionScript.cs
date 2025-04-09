using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OnClickBackButton()
    {
        State stateType = GameManager.previousState;
        GameManager.Instance.SwitchToPrevious();

    }
   

    
}
