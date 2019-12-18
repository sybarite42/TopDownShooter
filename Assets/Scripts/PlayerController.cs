using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerController : MonoBehaviour {

    [SerializeField]private Transform pfDashEffect;
    public float speed = 5f;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;


    public int maxHealth = 10;
    private int currentHealth;

    private bool hurt = false;

    private float xMovement = 0f;
    private float yMovement = 0f;


    private Animator animator;

    public SimpleHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        dashTime = startDashTime;
        transform.localPosition = new Vector3(transform.localPosition.x, 0.009f, transform.localPosition.z);
        
    }


    void Update () {


        HandleMovement();
        HandleDash();

        //healthBar.UpdateBar(currentHealth, maxHealth);

        //Player was hit
        if (hurt)
        {
            //Play hit animation
            animator.SetBool("Hurt", true);
        }


        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        

        

    }

    private void HandleMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal");

        yMovement = Input.GetAxisRaw("Vertical");


        if (xMovement != 0 || yMovement != 0)
        {
            //Play run animation
            animator.SetBool("Walking", true);
            Vector3 moveDir = new Vector3(xMovement, yMovement).normalized;

            if (TryMove(moveDir, speed * Time.deltaTime))
            {
                //Can move, no hitk
            }
            else
            {
                //Cannot move vertically
            }
        }
        else
        {
            //Stop run animation
            animator.SetBool("Walking", false);
        }
    }

    private bool TryMove(Vector3 baseMoveDir, float distance)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);
        if (!canMove)
        {
            //Cannot move diagonally
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = CanMove(moveDir, distance);
            if (!canMove)
            {
                //Cannot move horizontally
                moveDir = new Vector3(0f, baseMoveDir.y).normalized;
                canMove = CanMove(moveDir, distance);
            }
        }
        if (canMove)
        {
            //Can move, no hit
            transform.position += moveDir * distance;
            return true;
        }
        else
        {
            //Cannot move vertically
            return false;
        }

    }

    private bool CanMove(Vector3 dir, float distance)
    {
        return Physics2D.Raycast(transform.position, dir, distance). collider == null;
    }

    private void HandleDash()
    {
        //Dash move
        if (Input.GetMouseButtonDown(1))
        { 
            float dashDistance = 3f;
            Vector3 beforeDashPosition = transform.position;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            if (TryMove(dir.normalized, dashDistance)) {
                Transform dashEffectTransform = Instantiate(pfDashEffect, beforeDashPosition, Quaternion.identity);
                dashEffectTransform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
                dashEffectTransform.localScale = new Vector3(0.6f, 0.35f, 1f);
                //float dashEffectWidth = 10f;
                //dashEffectTransform.localScale = new Vector3(dashDistance / dashEffectWidth,0.35f,1f);

            }


        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
        if (currentHealth < 1)
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
        if(currentHealth < maxHealth)
            currentHealth += hpToAdd;
    }


}
