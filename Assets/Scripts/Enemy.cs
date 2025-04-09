using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Damagable
{
    public GameObject Player, diamond_prefab, blood, diamond_pool, damage_text,damage_text_parent;
    protected Vector3 direction, playerPos;
    [SerializeField] public int speed = 2;
    [SerializeField] public float Range = 4f;
    public List<Vector3> directions = new List<Vector3>();
    public int GivenDamage;

    public void EnemyUpdate()
    {
        EnemyMovement();
    }

    public virtual void EnemyMovement()
    {
        playerPos = Player.transform.position;
    }

    public virtual void Patrol()
    {
        int rand = Random.Range(0, 4);
        direction = directions[rand];
    }

    protected bool InRange()
    {
        float distance = (playerPos - transform.position).magnitude;
        if (distance < Range) return true;
        else return false;
    }

    protected void Move(Vector3 targetPos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    public override void Death()
    {
        // spawning diamonds (xp)
        GameObject diamond = Instantiate(diamond_prefab, diamond_pool.transform);
        diamond.transform.position = transform.position;
        // bloodeffect
        GameObject blood_gO = Instantiate(blood,transform.position, Quaternion.identity);
        Player.GetComponent<Player>().EnemyKilled += 1;
        base.Death();
    }
    public override void TakeDamage(int damage)
    {
        // Damage Text
        GameObject text = Instantiate(damage_text, damage_text_parent.transform);
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        text.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        
        // takedamage
        base.TakeDamage(damage);
    }

}
