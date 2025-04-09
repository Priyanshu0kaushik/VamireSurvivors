using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : StateMachine
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       SwitchState<MainMenuState>();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachineUpdate();
    }
}
