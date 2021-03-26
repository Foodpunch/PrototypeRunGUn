using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<GameObject> EnemyPrefabs;
    public List<BaseEntity> SpawnedEnemies;

    [SerializeField]
    int enemyAttackLimit = 1;

    float gameTime;
    float nextSpawnTime;
    float spawnInterval=2f;

    int spawnCap = 4;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime > nextSpawnTime && SpawnedEnemies.Count < spawnCap)
        {
            nextSpawnTime = gameTime + spawnInterval;
            SpawnEnemy();
        }

    }
    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, EnemyPrefabs.Count);
        if(EnemyPrefabs !=null)
        {
            GameObject enemyClone = Instantiate(EnemyPrefabs[randomEnemy],transform.position,Quaternion.identity);
            SpawnedEnemies.Add(enemyClone.GetComponent<BaseEntity>());
        }
    }
}
