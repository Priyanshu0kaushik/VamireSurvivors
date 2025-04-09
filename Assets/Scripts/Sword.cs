using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    Vector3 lowerBound, upperBound;
    float x, y;
    public Vector3 velocity;

    public void WeaponUpdate()
    {

        if (gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        }
        posCheck();
    }

    void posCheck()
    {
        lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)) - new Vector3(1, 1, 0);
        upperBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)) + new Vector3(1, 1, 0);
        x = transform.position.x;
        y = transform.position.y;

        if (((x < lowerBound.x) || (x > upperBound.x)) || ((y < lowerBound.y) || (y > upperBound.y)))
        {
            gameObject.SetActive(false);
        }
    }
}
