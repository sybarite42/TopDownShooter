using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public int selectedWeapon = 0;

	void Start()
	{
		SelectWeapon();
	}

	void Update()
	{
		var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if(angle > 90 || angle < -90)
		{
			FindObjectOfType<PlayerController>().transform.localScale = new Vector3(-1, 1, 1);
			GetComponentInChildren<Transform>().localScale = new Vector3(1, 1, 1);
			transform.localScale = new Vector3(1, -1, 1);
		}
		else
		{
			FindObjectOfType<PlayerController>().transform.localScale = new Vector3(1, 1, 1);
			GetComponentInChildren<Transform>().localScale = new Vector3(-1, 1, 1);
			transform.localScale = new Vector3(-1, 1, 1);
		}


		int previousSelectedWeapon = selectedWeapon;

		//Scroll down
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			//If at last weapon, loop around to the first
			if (selectedWeapon >= transform.childCount - 1)
				selectedWeapon = 0;
			else
				selectedWeapon++;
		}

		//Scroll up
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (selectedWeapon <= 0)
				selectedWeapon = transform.childCount - 1;
			else
				selectedWeapon--;
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			selectedWeapon = 0;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
		{
			selectedWeapon = 1;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
		{
			selectedWeapon = 2;
		}

        if(transform.childCount == 1)
        {
            SelectWeapon();
        }

		if (previousSelectedWeapon != selectedWeapon)
		{
			SelectWeapon();
		}

	}

	void SelectWeapon()
	{
		int i = 0;
		foreach(Transform weapon in transform)
		{
			if (i == selectedWeapon)
				weapon.gameObject.SetActive(true);
			else
				weapon.gameObject.SetActive(false);
			i++;
		}
	}


	public void Shoot(GameObject bullet, Transform firePoint, float spread, AudioSource noise)
	{
		GameObject bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);

		bulletInst.transform.Rotate(0, 0, Random.Range(-spread, spread));

		noise.Play(0);

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
