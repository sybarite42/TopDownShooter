using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {


	public Transform firePoint;
	public BulletController bullet;

    public float minSpread;
    public float maxSpread;
    public float currentSpread;

    private AudioSource pistolNoise;
    private float coolDown;




	void Start()
	{
		currentSpread = minSpread;
        pistolNoise = GetComponent<AudioSource>();
	}

	void Update () {

		if (currentSpread > minSpread)
			currentSpread -= Time.deltaTime ;

		if (coolDown > 0)
			coolDown -= Time.deltaTime;


		if (Input.GetButtonDown("Fire1") && coolDown <= 0)
		{
			coolDown = 0.1f;
			Shoot();
			if(currentSpread < maxSpread)
			{
				currentSpread++;
			}
		}
	}

	void Shoot()
	{
		BulletController bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);

		bulletInst.transform.Rotate(0, 0, Random.Range(-currentSpread, currentSpread));

        pistolNoise.Play(0);

	}


	/*
	void ShootRay()
	{
		firePoint = transform.position;



		Debug.DrawRay(firePoint, targetPos * range, Color.blue, 0.2f);

		laserLine.SetPosition(0, firePoint);

		RaycastHit2D hit = Physics2D.Raycast(firePoint, targetPos, range, whatToHit);

		laserLine.SetPosition(0, firePoint);

		if(hit.collider != null)
		{
			Debug.Log("Hit");
		}
	}
	*/




}
