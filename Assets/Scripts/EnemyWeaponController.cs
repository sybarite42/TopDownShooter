using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{

    public Transform firePoint;
    public BulletController bullet;

    public float weaponSpread = 10;

    private AudioSource pistolNoise;
    private float coolDown;
    private EnemyController controller;




    void Start()
    {
        pistolNoise = GetComponent<AudioSource>();
        controller = transform.root.GetComponent<EnemyController>();
    }

    void Update()
    {
        //If the Enemy sees the player
        if (controller.FoundPlayer())
        {

        
        if (coolDown > 0)
            coolDown -= Time.deltaTime;

        if (coolDown <= 0)
        {
            coolDown = 0.3f;
            Shoot();
            }
        }
    }

    void Shoot()
    {
        BulletController bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);

        bulletInst.transform.Rotate(0, 0, Random.Range(-weaponSpread, weaponSpread));

        pistolNoise.Play(0);

    }
}
