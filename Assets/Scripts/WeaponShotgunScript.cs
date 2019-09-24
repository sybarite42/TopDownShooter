using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgunScript : MonoBehaviour
{

    private float coolDown = 0.7f;
    private float coolDownTimer;

    public float maxSpread = 30f;
    private float currentSpread;

    private int numberOfPellets = 8;

    public GameObject bullet;
    public Transform firePoint;

    private AudioSource shootNoise;

    private WeaponController controller;


    void Start()
    {
        controller = GetComponentInParent<WeaponController>();
        shootNoise = GetComponent<AudioSource>();
    }

    void Update()
    {


        

        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;


        if (Input.GetButtonDown("Fire1") && coolDownTimer <= 0)
        {
            
            for (int i = 0; i < numberOfPellets; i++)
            {
                currentSpread = Random.Range(-maxSpread, maxSpread);
                controller.Shoot(bullet, firePoint, currentSpread, shootNoise);
            }

            coolDownTimer = coolDown;

        }
    }
}
