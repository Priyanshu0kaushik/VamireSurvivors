using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void OptionOnClick()
    {
        GameManager.Instance.SwitchState<OptionMenuState>();
    }

    public void StartOnClick()
    {
        GameManager.Instance.SwitchState<PlayingState>();
    }
}
