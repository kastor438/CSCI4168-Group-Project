using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody2D RB2D;
    public Vector3 direction;
    public float spawnTime;
    public float projectileLifetime;
    public RangedWeapon rangedWeapon;
    void Update()
    {
        if(Time.unscaledTime > spawnTime + projectileLifetime)
        {
            Destroy(gameObject);
        }
        RB2D.velocity *= 0.999f;
    }
    
    public virtual void SetProjectileInfo(Vector3 direction, RangedWeapon rangedWeapon)
    {
        this.rangedWeapon = rangedWeapon;
        RB2D = GetComponent<Rigidbody2D>();
        spawnTime = Time.unscaledTime;
        this.direction = direction;
        this.projectileLifetime = rangedWeapon.projectileLifetime;
        RB2D.velocity = direction * rangedWeapon.projectileSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with " + collision.name);
        if (!collision.CompareTag("Player") && !collision.CompareTag("PlayerBody") && !collision.CompareTag("FriendlyProjectile") && !collision.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStats>().TakeDamage(rangedWeapon.damage);
        }
    }
}
