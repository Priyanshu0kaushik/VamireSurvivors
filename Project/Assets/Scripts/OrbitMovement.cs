using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public GameObject target;
    public float orbitRadius, movementSpeed;
    public float Angle;

    public void UpgradeUpgate()
    {
        move();
    }

    void move()
    {
        float x = target.transform.position.x + Mathf.Cos(Angle) * orbitRadius;
        float y = target.transform.position.y + Mathf.Sin(Angle) * orbitRadius;
        
        transform.position = new Vector3(x,y,0);
        Angle += Time.deltaTime * movementSpeed;
    }
}
