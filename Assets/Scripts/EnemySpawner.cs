using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject target, enemyPool, diamondPool, damageTextParent;

    [SerializeField] int waveSize = 20, currentWave = 0;
    private Vector2 spawnPos;
    Vector2 lowerBound, upperBound;
    float minX, maxX, minY, maxY, spawnX, spawnY;
    [SerializeField] float offset = 3f;
    [SerializeField] float timeBetweenWaves = 30f, timeElapsed = 28f;

    public EnemyContainer container;

    public List<List<GameObject>> Enemies = new List<List<GameObject>>();

    public void EnemyRestart()
    {
        foreach (List<GameObject> list in Enemies)
        {
            foreach (GameObject e in list)
            {
                e.SetActive(false);
            }
        }
        foreach (Transform diamond in diamondPool.transform) Destroy(diamond?.transform.gameObject);
        timeElapsed = 28f;
        currentWave = 1;
    }

    public void EnemySpawnerStart()
    {
        for (int i = 0; i < container.Enemies.Count; i++)
        {
            Enemies.Add(new List<GameObject>());
        }
 
    }
    public void EnemySpawnUpdate()
    {
        // spawns faster as waves passes 
        SpawnWavesOverTime(Mathf.Max(timeBetweenWaves / currentWave, 0.5f));
    }

    void SpawnWavesOverTime(float time)
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > time)
        {
            timeElapsed = 0f;
            SpawnEnemies();
            currentWave++;
        }       
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < waveSize; i++)
        {
            int rand = Random.Range(0, container.Enemies.Count);
            GameObject enemy;
            enemy = GetPooledObject(rand);
            enemy.transform.position = GetSpawnPos();
            if (enemy.GetComponent<AttackRangeEnemy>() != null)
            {
                enemy.GetComponent<AttackRangeEnemy>().Patrol();
            }
        }
    }

    GameObject GetPooledObject(int index)
    {
        foreach (GameObject enemy in Enemies[index])
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        GameObject Enemy;
        Enemy = Instantiate(container.Enemies[index].EnemyPrefab, enemyPool.transform) ;
        Enemies[index].Add(Enemy);
        Enemy.GetComponent<Enemy>().Player = target;
        Enemy.GetComponent<Enemy>().diamond_pool = diamondPool;
        Enemy.GetComponent<Enemy>().directions.AddRange(new Vector3[] { Vector3.up, Vector3.right, Vector3.left, Vector3.down });
        Enemy.GetComponent<Enemy>().damage_text_parent = damageTextParent;
        GetPrefabs(Enemy);
        return Enemy;
    }

    Vector3 GetSpawnPos()
    {
        // making sure enemy spawn outside the cameraView
        lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)) - new Vector3(1, 1, 0);
        upperBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)) + new Vector3(1, 1, 0);
        minX = lowerBound.x - offset;
        maxX = upperBound.x + offset;
        minY = lowerBound.y - offset;
        maxY = upperBound.y + offset;
        do
        {
            spawnX = Random.Range(minX, maxX);
            spawnY = Random.Range(minY, maxY);
        }while (((spawnX>lowerBound.x) && (spawnX<upperBound.x)) && ((spawnY > lowerBound.y) && (spawnY < upperBound.y)));

        return new Vector3(spawnX,spawnY,0);
    }

    void GetPrefabs(GameObject e)
    {
        e.GetComponent<Enemy>().damage_text = (GameObject)Resources.Load("Prefabs/DamageText");
        e.GetComponent<Enemy>().diamond_prefab = (GameObject)Resources.Load("Prefabs/Diamond");
        e.GetComponent<Enemy>().blood = (GameObject)Resources.Load("Prefabs/Blood");
    }

    
}
