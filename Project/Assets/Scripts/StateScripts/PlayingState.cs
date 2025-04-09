using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    [SerializeField] Player Player;

    [SerializeField] WeaponSpawn WeaponSpawn;
    bool executed =false;
    private Color green = new Color(0.2933429f, 0.6037736f, 0.3567642f, 1), blue = new Color(0.130696f, 0.1686362f, 0.3113208f, 1);

    [SerializeField] EnemySpawner EnemySpawner;


    public override void EnterState()
    {
        gameObject.SetActive(true);

        Camera.main.backgroundColor = green;

        if (StateMachine.restart)
        {
            Player.PlayerRestart();
            EnemySpawner.EnemyRestart();
            WeaponSpawn.WeaponRestart();
        }
        else
        {
            EnemySpawner.EnemySpawnerStart();    
        }
        if (!executed)
        {
            WeaponSpawn.WeaponInstantiate();
            executed = true;
        }
        hasEnteredState = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Player.PlayerUpdate();

        WeaponSpawn.WeaponSpawnUpdate();
        EnemySpawner.EnemySpawnUpdate();

        foreach (List<GameObject> list in EnemySpawner.Enemies)
        {
            foreach (GameObject enemy in list)
            {
                if (enemy.activeInHierarchy)
                {
                    enemy.GetComponent<Enemy>().EnemyUpdate();
                }
            }
        }
        foreach (GameObject sword in WeaponSpawn.Swords)
        {
            if (sword.activeInHierarchy)
            {
                sword.GetComponent<Sword>().WeaponUpdate();
            }
        }

    }

    public override void ExitState()
    {
        base.ExitState();
        Camera.main.backgroundColor = blue;
        StateMachine.restart = true;

    }
}
