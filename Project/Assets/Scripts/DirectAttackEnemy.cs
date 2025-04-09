using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectAttackEnemy : Enemy
{
    public override void EnemyMovement()
    {
        base.EnemyMovement();
        base.Move(playerPos);
    }
}
