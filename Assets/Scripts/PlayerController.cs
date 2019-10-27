using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 5f;

    private int health = 10;
    private bool hurt = false;

    private float xMovement = 0f;
    private float yMovement = 0f;


    private Rigidbody2D rb;             //Rigidbody2D

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update () {

        //Player was hit
        if (hurt)
        {
            //Play hit animation
            animator.SetBool("Hurt", true);
        }


        if (health <= 0)
        {
            Destroy(gameObject);
        }


    }

    void FixedUpdate()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        if (xMovement > 0f)
        {
            rb.velocity = new Vector2(xMovement * speed, rb.velocity.y);
        }
        else if (xMovement < 0f)
        {
            rb.velocity = new Vector2(xMovement * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        yMovement = Input.GetAxisRaw("Vertical");

        if (yMovement > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, yMovement * speed);
        }
        else if (yMovement < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, yMovement * speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,0);

        }

        if(xMovement != 0 || yMovement != 0)
        {
            //Play run animation
            animator.SetBool("Walking", true);
        }
        else
        {
            //Stop run animation
            animator.SetBool("Walking", false);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void FinishHurt()
    {
        animator.SetBool("Hurt", false);
    }

    //Play as event after hurt animation to check if was last blow
    // if it was play death animation
    public void CheckDeath()
    {
        //Check health if at 0 or less hp
        if (health < 1)
        {
            animator.SetBool("Death", true);

            //Disable object's scripts
            MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = false;
            }
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
