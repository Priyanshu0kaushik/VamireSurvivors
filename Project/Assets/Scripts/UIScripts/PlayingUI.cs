using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingUI : MonoBehaviour
{
    public void PauseOnClick()
    {
        GameManager.Instance.SwitchState<PausedState>();
    }
}
