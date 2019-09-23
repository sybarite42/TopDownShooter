using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMachineGunScript : MonoBehaviour
{


    public float minSpread;
    public float maxSpread;
    private float currentSpread;

    private float coolDown;
    public float coolDownTimer;

    public BulletController bullet;
    public Transform firePoint;

    private AudioSource shootNoise;

    private WeaponController controller;


    void Start()
    {
        controller = GetComponentInParent<WeaponController>();
        shootNoise = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        maxSpread = 6f;
        coolDown = 0.1f;
        currentSpread = minSpread;
    }


    void Update()
    {


        if (currentSpread > minSpread)
            currentSpread -= Time.deltaTime;

        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;


        if (Input.GetButton("Fire1") && coolDownTimer <= 0)
        {
            coolDownTimer = coolDown;
            controller.Shoot(bullet, firePoint, currentSpread, shootNoise);

            if (currentSpread < maxSpread)
            {
                currentSpread++;
            }
        }
    }
}
