using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 5f;

    private int health = 10;
    private float movement = 0f;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update () {

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal");
        if (movement > 0f)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        else if (movement < 0f)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        movement = Input.GetAxisRaw("Vertical");

        if (movement > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, movement * speed);
        }
        else if (movement < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, movement * speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,0);

        }
    }

        public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
