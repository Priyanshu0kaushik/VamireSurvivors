using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject SwordPrefab;
    [SerializeField] float swordSpeed = 5f;
    Vector3 Direction;
    GameObject sword;
    public GameObject SwordPool;
    public List<GameObject> Swords = new List<GameObject>();
    Vector3 originalSize = new Vector3(0.81f, 0.81f, 0.81f);
    Vector3 size = new Vector3();
    public void WeaponRestart()
    {
        foreach(GameObject sword in Swords)
        {
            sword.gameObject.SetActive(false);
            size = originalSize;

        }
    }
    public void WeaponInstantiate()
    {
        size = originalSize;
        GameObject sword;
        for(int i = 0; i < 5; i++)
        {
            sword = Instantiate(SwordPrefab, SwordPool.transform);
            sword.transform.localScale = originalSize;
            Swords.Add(sword);
            sword.SetActive(false);
            
        }
    }
    public void WeaponSpawnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Vector2 Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Direction = Mousepos - (Vector2)transform.position;

            sword = GetPooledObject();
            sword.transform.localScale = size;
            sword.transform.position = transform.position;
            sword.SetActive(true);
            sword.transform.up = Direction.normalized;

            sword.GetComponent<Sword>().velocity = swordSpeed * Direction.normalized;
            sword.GetComponent<Rigidbody2D>().velocity = swordSpeed * Direction.normalized;

        }
    }

    public void BiggerWeapon(Vector3 addSize)
    {
        size += addSize;
    }

    GameObject GetPooledObject()
    {
        foreach(GameObject sword in Swords)
        {
            if (!sword.activeInHierarchy)
            {
                return sword;
            }
        }
        GameObject new_sword = Instantiate(SwordPrefab, SwordPool.transform);
        Swords.Add(new_sword);
        new_sword.transform.localScale = size;
        new_sword.SetActive(false);
        return new_sword; 
    }
}
