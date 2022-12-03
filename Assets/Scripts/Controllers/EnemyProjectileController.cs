using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Rigidbody2D RB2D;
    private Vector3 direction;
    private float spawnTime;
    private float projectileLifetime = .1f;
    private int projectileDamage = 10;
    private Transform player;
    private Vector2 target;
    public int speed;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }
    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (transform.position.x == target.x)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collided with " + collision.name);
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("EnemyBody") && !collision.CompareTag("FriendlyProjectile") 
            && !collision.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerBody"))
        {
            collision.GetComponentInParent<PlayerStats>().TakeDamage(projectileDamage);
            Destroy(gameObject);

        }
    }
}
