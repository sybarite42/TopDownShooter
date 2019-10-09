using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemySoldierScript : MonoBehaviour
{

    private GameObject target;

    private int maxHealth = 5;
    public int rotateSpeed = 1;

    private float coolDown;

    public float weaponSpread = 10;

    public Transform firePoint;
    public GameObject bullet;
    
    private AIDestinationSetter aiSetter;
    private EnemyWeaponController weapon;

    void Start()
    {
        GetComponent<EnemyController>().SetHealth(maxHealth);
        target = GameObject.Find("Player");
        aiSetter = GetComponent<AIDestinationSetter>();
        aiSetter.target = target.transform;
        weapon = GetComponentInChildren<EnemyWeaponController>();
    }

    void Update()
    {



        if (coolDown > 0)
            coolDown -= Time.deltaTime;

        //gets the vector to the target
        Vector2 targetPos = target.transform.position - transform.position;

        //Look towards player
        transform.right = targetPos;

        //Draw ray from enemy to player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos);

        //Has sight on player and close by
        if (hit.collider.gameObject.tag == "Player" && Vector2.Distance(transform.position, hit.transform.position) < 15)
        {
            
            //Attack
            if (coolDown <= 0)
            {
                coolDown = 1f;
                weapon.Shoot(bullet, firePoint, weaponSpread);
            }

        }
        //Player not in LOS or range
        else
        {

        }
    }
}
