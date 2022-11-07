using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D RB2D;
    private Vector3 direction;
    private float spawnTime;
    private float projectileLifetime;

    void Update()
    {
        if(Time.unscaledTime > spawnTime + projectileLifetime)
        {
            Destroy(gameObject);
        }
        RB2D.velocity *= 0.999f;
    }
    
    public void SetProjectileInfo(Vector3 direction, float projectileSpeed, float projectileLifetime)
    {
        RB2D = GetComponent<Rigidbody2D>();
        spawnTime = Time.unscaledTime;
        this.direction = direction;
        this.projectileLifetime = projectileLifetime;
        RB2D.velocity = direction * projectileSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with " + collision.name);
        if (!collision.CompareTag("Player") && !collision.CompareTag("PlayerBody") && !collision.CompareTag("FriendlyProjectile") && !collision.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
        }
    }
}
