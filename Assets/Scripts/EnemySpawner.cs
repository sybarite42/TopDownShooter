using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public bool obstructed = false;

    public void SpawnEnemy(EnemyController enemy)
    {
        Instantiate(enemy,transform.position, transform.rotation);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        obstructed = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        obstructed = false;
    }

}
