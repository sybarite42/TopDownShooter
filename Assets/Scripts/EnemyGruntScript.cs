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

    public AIPath aiPath;
    private AIDestinationSetter aiSetter;

    private Animator animator;

    void Start()
    {
        GetComponent<EnemyController>().SetHealth(maxHealth);
        target = GameObject.Find("Player");
        aiPath = GetComponent<AIPath>();
        aiSetter = GetComponent<AIDestinationSetter>();
        aiSetter.target = target.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

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

        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-4f, 4f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(4f, 4f, 1f);
        }

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
