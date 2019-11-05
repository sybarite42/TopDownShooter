using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 5f;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;



    public int health = 10;
    private bool hurt = false;

    private float xMovement = 0f;
    private float yMovement = 0f;
    private Vector3 lastMoveDir;


    private Rigidbody2D rb;             //Rigidbody2D

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dashTime = startDashTime;
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

        xMovement = Input.GetAxisRaw("Horizontal");

        yMovement = Input.GetAxisRaw("Vertical");
        

        if(xMovement != 0 || yMovement != 0)
        {
            //Play run animation
            animator.SetBool("Walking", true);
            Vector3 moveDir = new Vector3(xMovement, yMovement).normalized;

            Vector3 targetMovePosition = transform.position + moveDir * speed * Time.deltaTime;
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moveDir, speed * Time.deltaTime);

            if(raycastHit.collider == null)
            {
                //Can move, no hit
                lastMoveDir = moveDir;
                transform.position = targetMovePosition;
            }
            else
            {
                //Cannot move diagonally, hit something

                //Test moving in horizontal direction
                Vector3 testMoveDir = new Vector3(moveDir.x, 0f).normalized;
                targetMovePosition = transform.position + testMoveDir * speed * Time.deltaTime;
                raycastHit = Physics2D.Raycast(transform.position, testMoveDir, speed * Time.deltaTime);
                if(testMoveDir.x !=0f && raycastHit.collider == null)
                {
                    //Can move horizontally
                    lastMoveDir = testMoveDir;
                    transform.position = targetMovePosition;
                }
                else
                {
                    //Cannot move horizontally
                    testMoveDir = new Vector3(0f, moveDir.y).normalized;
                    targetMovePosition = transform.position + testMoveDir * speed * Time.deltaTime;
                    raycastHit = Physics2D.Raycast(transform.position, testMoveDir, speed * Time.deltaTime);
                    if(testMoveDir.y != 0f && raycastHit.collider == null)
                    {
                        //Can move vertically
                        lastMoveDir = testMoveDir;
                        transform.position = targetMovePosition;
                    }
                    else
                    {
                        //Cannot move vertically

                    }
                }
            }
        }
        else
        {
            //Stop run animation
            animator.SetBool("Walking", false);
        }

        //Dash move
        if (Input.GetMouseButtonDown(1) )
        {

            if(rb.velocity == new Vector2(0,0))
            {
                float dashDistance = 3f;
                var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

                transform.position += dir.normalized * dashDistance;
                
                
                
            }
            else
            {

            }
            
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

    public void AddHealth(int hpToAdd)
    {
        health += hpToAdd;
    }


}
