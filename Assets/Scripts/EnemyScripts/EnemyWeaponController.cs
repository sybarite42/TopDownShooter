using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    private AudioSource noise;

    void Start()
    {
        noise = GetComponent<AudioSource>();
    }


    public void Shoot(GameObject bullet, Transform firePoint, float spread)
    {
        GameObject bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);

        bulletInst.transform.Rotate(0, 0, Random.Range(-spread, spread));

        noise.Play(0);

    }
}
