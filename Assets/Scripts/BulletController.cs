using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    public float lifeTime;

    public int damage;

    // Update is called once per frame

    void Start()
    {
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

        
        
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }


        Destroy(gameObject);

    }

}
