using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{

    private float speed;

    private float lifeTime = 2f;

    private int damage = 1;

    // Update is called once per frame

    void Start()
    {
        speed = Random.Range(18f, 22f);
    }

    void Update()
    {

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyController>().GetHealth() > 0)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }

        /*
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        */
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyController>().GetHealth() > 0)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                float thrust = 25;

                Vector2 difference = enemyRb.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemyRb.AddForce(difference, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
