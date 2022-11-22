using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Rigidbody2D RB2D;
    private Vector3 direction;
    private float spawnTime;
    private float projectileLifetime;
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
        /*RB2D = GetComponent<Rigidbody2D>();
        if(Time.unscaledTime > spawnTime + projectileLifetime)
        {
            Destroy(gameObject);
        }
        RB2D.velocity *= 0.999f;*/
    }
    
    /*public void SetProjectileInfo(Vector3 direction)
    {
        RB2D = GetComponent<Rigidbody2D>();
        spawnTime = Time.unscaledTime;
        this.direction = direction;
        this.projectileLifetime = 50;
        RB2D.velocity = direction * 5;
    } */

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with " + collision.name);
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("EnemyBody") && !collision.CompareTag("FriendlyProjectile") 
            && !collision.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerBody"))
        {
            Debug.Log("Damage player");
            collision.GetComponentInParent<PlayerStats>().TakeDamage(projectileDamage);
        }
    }
}
