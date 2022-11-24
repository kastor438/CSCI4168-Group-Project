using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    private Rigidbody2D RB2D;
    private Vector3 direction;
    private float spawnTime;
    private float projectileLifetime;
    private RangedWeapon rangedWeapon;

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
        RB2D.velocity = this.direction * rangedWeapon.projectileSpeed;
        for(int i = 0; i < GameManager.Instance.followList.Count; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.followList[i].GetComponent<Collider2D>());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with " + collision.name);
        if (collision.CompareTag("EnemyBody"))
        {
            collision.GetComponentInParent<EnemyStats>().TakeDamage(rangedWeapon.damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
