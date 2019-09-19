using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	private GameObject player;        //Stores a reference to the player game object

	private Vector3 offset;            //Stores the offset distance between the player and camera


	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        
    }
	

	void Update () {
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		transform.position = player.transform.position + offset;
	}
}
