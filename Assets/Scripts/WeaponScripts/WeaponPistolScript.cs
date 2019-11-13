﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistolScript : MonoBehaviour
{

    
    public float minSpread;
    public float maxSpread = 4f;
    private float currentSpread;

    private float coolDown = 0.2f;
    private float coolDownTimer;

    public GameObject bullet;
    public Transform firePoint;

    private AudioSource shootNoise;

    private WeaponController controller;


    void Start()
    {
        controller = GetComponentInParent<WeaponController>();
        shootNoise = GetComponent<AudioSource>();
        currentSpread = minSpread;
    }

    void Update()
    {


        if (currentSpread > minSpread)
            currentSpread -= Time.deltaTime;

        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;


        if (Input.GetButtonDown("Fire1") && coolDownTimer <= 0)
        {
            coolDownTimer = coolDown;
            controller.Shoot(bullet,firePoint,currentSpread, shootNoise);

            if (currentSpread < maxSpread)
            {
                currentSpread++;
            }
        }
    }

}