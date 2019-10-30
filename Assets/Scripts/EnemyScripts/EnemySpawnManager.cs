using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{


    public GameObject enemy1;

    public EnemySpawner [] spawners;

    public int currentEnemyCount;
    public int maxEnemyCount;

    private float coolDown = 1f;
    private float coolDownTimer = 0f;


    void Start()
    {
        spawners = GetComponentsInChildren<EnemySpawner>();
    }


    // Update is called once per frame
    void Update()
    {
        if(currentEnemyCount < maxEnemyCount && coolDownTimer <= 0)
        {
            int spawnNumber = Random.Range(0, 3);
            if (!spawners[spawnNumber].obstructed)
            {
                spawners[spawnNumber].SpawnEnemy(enemy1);
                coolDownTimer = coolDown;
            }
        }

        coolDownTimer -= Time.deltaTime;

        currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

}
