using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : AttackerEnemy
{
    private float shootingCooldown;
    public float timeShootingCooldown;
    public GameObject projectile;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackRange = 3;
        shootingCooldown = timeShootingCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack() {
        Debug.Log("Attack");
        if (shootingCooldown <= 0) 
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shootingCooldown = timeShootingCooldown;
            Debug.Log("Shoot");

        } else {
            shootingCooldown -= Time.deltaTime;
        }
    }
}
