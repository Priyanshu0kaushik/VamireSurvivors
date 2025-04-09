using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedUI : MonoBehaviour
{
    public void OnClickResume()
    {
        GameManager.Instance.SwitchState<PlayingState>();
    }

    public void OnClickMainMenu()
    {
        GameManager.Instance.SwitchState<MainMenuState>();
    }

    public void OnClickOptions()
    {
        GameManager.Instance.SwitchState<OptionMenuState>();
    }
}
