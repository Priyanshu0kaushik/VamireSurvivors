using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateState : State
{
    public UpdateManager UpdateManager;
    public override void EnterState()
    {   
        UpdateManager.ChooseRandomUpgrade();
        base.EnterState();

    }
}
