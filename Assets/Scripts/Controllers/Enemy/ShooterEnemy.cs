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
        if (shootingCooldown <= 0) 
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shootingCooldown = timeShootingCooldown;
            Debug.Log("Instantiate should be running if you can read this");

        } else {
            shootingCooldown -= Time.deltaTime;
        }
    }
}
