using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DieState : State
{
    public int HighScore = 0;
    public TextMeshProUGUI highScore_text;
    public GameObject Player;

    public void OnClickRestart()
    {
        GameManager.Instance.SwitchState<PlayingState>();
    }
    public void OnClickMainMenu()
    {
        GameManager.Instance.SwitchState<MainMenuState>();
    }

    public override void EnterState()
    {
        HighScore = (HighScore < Player.GetComponent<Player>().EnemyKilled) ? Player.GetComponent<Player>().EnemyKilled : HighScore;
        highScore_text.text = "High Score (Enemy Killed): " + HighScore.ToString();
        base.EnterState();
    }
}
