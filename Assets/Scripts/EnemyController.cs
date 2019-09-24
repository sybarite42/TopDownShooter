using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour {
    
    private GameObject target;
    private bool foundPlayer = true;

    public int health = 5;
    public int rotateSpeed = 6;
    private float flashCounter = 0f;
    private SpriteRenderer sRend;       //SpriteRenderer
    public Sprite originalSprite;
    public Sprite whiteSprite;

    private AIDestinationSetter aiSetter;


    void Start()
    {
        sRend = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player");
        aiSetter = GetComponent<AIDestinationSetter>();
        aiSetter.target = target.transform;
        //aiSetter.enabled = false;
    }

    void Update(){

        //Player hit
        if (flashCounter > 0)
        {
            sRend.sprite = whiteSprite;
            flashCounter -= Time.deltaTime;
        }

        //Player no longer hit
        if (flashCounter < 0)
        {
            sRend.sprite = originalSprite;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        Vector2 targetPos = target.transform.position - transform.position;

        transform.right = targetPos;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos);

        //Has sight on player
        if (hit.collider.gameObject.tag == "Player" && Vector2.Distance(transform.position, hit.transform.position) < 15)
        {
            foundPlayer = true;
            //aiSetter.enabled = true;

        }
        
        else
        {
            foundPlayer = false;
        }

    }

    public bool FoundPlayer()
    {
        return foundPlayer;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        flashCounter = 0.3f;
    }
}
