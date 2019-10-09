using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGruntScript : MonoBehaviour
{

    private GameObject target;

    private int maxHealth = 5;
    public int rotateSpeed = 1;

    private float coolDown;
    private float coolDownTimer;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    private int damage = 1;

    //Stores the previously or currently stored health
    private int storedHealth;

    public AIPath aiPath;
    private AIDestinationSetter aiSetter;

    private Animator animator;

    void Start()
    {
        GetComponent<EnemyController>().SetHealth(maxHealth);
        storedHealth = maxHealth;
        target = GameObject.Find("Player");
        aiPath = GetComponent<AIPath>();
        aiSetter = GetComponent<AIDestinationSetter>();
        aiSetter.target = target.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //Object was hit
        if(GetComponent<EnemyController>().GetHealth() < storedHealth)
        {
            //Play hit animation
            animator.SetBool("Hurt", true);
            //reset stored health
            storedHealth = GetComponent<EnemyController>().GetHealth();
        }

        //Check health from controller
        if(GetComponent<EnemyController>().GetHealth() < 1)
        {
            animator.SetBool("Death", true);

            //Disable object's scripts
            MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = false;
            }
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            

        }

        //Check if reached the destination
        if (aiPath.reachedDestination)
        {
            
            animator.SetBool("Walking", false);
            //Attack
            animator.SetBool("Attacking", true);
            if(transform.position.x > target.transform.position.x)
                transform.localScale = new Vector3(4f, 4f, 1f);
            else
                transform.localScale = new Vector3(-4f, 4f, 1f);

        }
        else
        {
            animator.SetBool("Walking", true);
        }

        //Check which way travelling to face direction
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-4f, 4f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(4f, 4f, 1f);
        }


        //Attack cool down
        if (coolDown > 0)
            coolDown -= Time.deltaTime;

        //gets the vector to the target
        //Vector2 targetPos = target.transform.position - transform.position;

        //Look towards player
        //transform.right = targetPos;

    }


    public void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<PlayerController>().TakeDamage(damage);
        }
    }


    public void FinishAttacking()
    {
        animator.SetBool("Attacking", false);
    }

    public void FinishHurt()
    {
        animator.SetBool("Hurt", false);
    }

    public void FinishSpawning()
    {
        animator.SetBool("Spawned", true);
        //Enable object's scripts
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = true;
        }
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
