using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manages health and sprite changing
public class EnemyController : MonoBehaviour {

    private int health;

    void Start()
    {
        transform.rotation = Quaternion.identity;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void SetHealth(int hp)
    {
        health = hp;
    }

    public int GetHealth()
    {
        return health;
    }

}
