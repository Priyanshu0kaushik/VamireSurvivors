using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeEnemy : Enemy
{

    Vector3 targetPos;
    float moveDistance = 5;
    bool ismoving;

    public override void EnemyMovement()
    {
        base.EnemyMovement();
        if(ismoving)
        {
            MoveToTarget();
        }
    }
    public override void Patrol()
    {
        base.Patrol();
        targetPos = transform.position + base.direction * moveDistance;
        //Debug.Log(targetPos + " PatrolCalled"+transform.position);
        ismoving = true;
    }
    void MoveToTarget()
    {
        base.Move(targetPos);

        if (InRange()) targetPos = playerPos;
        else if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            ismoving = false;
            Invoke("Patrol",0.5f);
        }
        else { }
    }
}
