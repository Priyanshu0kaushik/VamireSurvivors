using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool hasEnteredState = false;
    public virtual void EnterState()
    {
        gameObject.SetActive(true);
        hasEnteredState = true;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {
        hasEnteredState = false;
        gameObject.SetActive(false);
    }
}
