using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manages health and sprite changing
public class EnemyController : MonoBehaviour {

    private int health;
    private float flashCounter;
    private SpriteRenderer sRend;       //SpriteRenderer
    public Sprite originalSprite;
    public Sprite whiteSprite;

    void Start()
    {

        sRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

        //Death
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        flashCounter = 0.3f;
    }

    public void SetHealth(int hp)
    {
        health = hp;
    }

}
