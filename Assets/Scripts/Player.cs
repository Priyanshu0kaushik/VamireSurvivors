using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class Player : Damagable
{

    public Image healthImage, levelImage;
    public int EnemyKilled = 0;
    public int MoveSpeed;
    float hInput, VInput;
    float timeElapsed1 = 0, exp = 0;
    public TextMeshProUGUI level_text;
    int Level = 1, angle = 0;
    [SerializeField] Animator animator;
    bool dead;
    public List<GameObject> AttachedUpgrades = new List<GameObject>();

    public void PlayerRestart()
    {
        ClearUpgrades();
        EnemyKilled = 0;
        AttachedUpgrades = new List<GameObject>();
        //resets player pos
        transform.position = Vector3.zero;
        // resetting the level and exp;
        Level = 1;
        level_text.text = "Level " + Level;
        exp = 0;
        levelImage.fillAmount = exp;
        // refilling the health and resetting incoming damage
        MaxHealth = 100;
        Health = MaxHealth;
        healthImage.fillAmount = 1;
        healthImage.color = new Color(1 - 1, 1, 0);
        dead = false;
    }


    public void PlayerUpdate()
    {
        animator.SetBool("Dead", dead);
        // Movement
        if (!dead)
        {
            playerMove();
            cameraMovement();
            foreach (GameObject upgrade in AttachedUpgrades)
            {
                upgrade.GetComponent<OrbitMovement>().UpgradeUpgate();
            }
        }
    }

    void playerMove()
    {
        hInput = Input.GetAxis("Horizontal");
        VInput = Input.GetAxis("Vertical");
        angle = GetAngle(hInput);
        PlayerTurn(angle);
        Vector3 pos = new Vector3(hInput, VInput, 0);

        if (pos.magnitude > 0.1f) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
        
        transform.position += pos.normalized * Time.deltaTime * MoveSpeed;
    }
    
    void PlayerTurn(int Angle)
    {
        Quaternion rotation = Quaternion.Euler(0, Angle, 0);
        transform.rotation = rotation;
    }
    int GetAngle(float input)
    {
        if (input == 0) return angle;
        return (input > 0) ? 0 : 180;
    }

    void cameraMovement()
    {
        float z = Camera.main.transform.position.z;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        float healthVal = (float)Health / MaxHealth;
        healthImage.fillAmount = healthVal;
        healthImage.color = new Color(1 - healthVal, healthVal, 0);

    }

    public override void Death()
    {
        Debug.Log("Player Dead");
        dead = true;
        GameManager.Instance.SwitchState<DieState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(collision.GetComponent<Enemy>().GivenDamage);
        }

        else if (collision.CompareTag("xp"))
        {
            exp += 0.7f / Level;
            if (exp > 1)
            {
                Level += 1;
                level_text.text = "Level " + Level;
                GameManager.Instance.SwitchState<UpdateState>();
                exp -= 1;
            }
            levelImage.fillAmount = exp;
            Destroy(collision.gameObject);

            

        }
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            timeElapsed1 += Time.deltaTime;
            if (timeElapsed1 >= 0.2f)                       // taking damage every 0.2 second
            {   

                TakeDamage(collision.GetComponent<Enemy>().GivenDamage);
                timeElapsed1 = 0;
            }
        }
    }

    void ClearUpgrades()
    {
        foreach(GameObject upgrade in AttachedUpgrades)
        {
            Destroy(upgrade);
        }
    }
}
