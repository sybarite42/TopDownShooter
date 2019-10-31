using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemScript : MonoBehaviour
{

    public GameObject weapon;

    private Transform weaponHolder;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponHolder = player.transform.GetChild(0);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= 1 && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(weapon, weaponHolder);
            Destroy(gameObject);
        }
    }
}
