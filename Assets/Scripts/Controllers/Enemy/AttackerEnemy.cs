using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AttackerEnemy : EnemyController
{
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    
    public float chaseRange;
    public float attackRange;

    public float nextWaypointDistance = 1f;
    Seeker seeker;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackRange = GetComponent<EnemyStats>().enemy.attackRange;
        chaseRange = GetComponent<EnemyStats>().enemy.chaseRange;
        seeker = GetComponent<Seeker>();

        //Specifies a method to be repeated
        InvokeRepeating("UpdatePath", 0f, .5f);
        

        if (chaseRange == 0) 
        {
                chaseRange = 5f;
        }
        if (attackRange == 0) 
        {
                attackRange = 2f;
        }
    }

    bool isInChaseRange() 
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !player)
        {
            player = GameManager.Instance.player;
            return false;
        }

        float distanceFromPlayer = Vector2.Distance(RB2D.position, player.transform.position);
        if (distanceFromPlayer <= chaseRange)
        {
            return true;
        }
        return false;
    }

    bool isInAttackRange() 
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !player)
        {
            player = GameManager.Instance.player;
            return false;
        }

        float distanceFromPlayer = Vector2.Distance(RB2D.position, player.transform.position);
        if (distanceFromPlayer <= attackRange)
        {
            RB2D.velocity *= 0.96f;
            if (RB2D.velocity.magnitude < 0.8f)
            {
                RB2D.velocity = Vector2.zero;
            }

            enemyAnimator.SetFloat("Velocity_Y", Vector3.Normalize(RB2D.velocity).y);
            enemyAnimator.SetFloat("Vertical", (transform.position.y - player.transform.position.y) > 0 ? -1 : 1);
            return true;
        }
        return false;
    }

    //Called when path is complete
    void OnPathComplete(Path p) 
    {
        if (!p.error) 
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (!player)
            return;
        if (seeker.IsDone()) //Checks if seeker is currently calculating a path
        {
            seeker.StartPath(RB2D.position, player.transform.position, OnPathComplete);
        }
    }

//Called a fixed number of time per seconds to properly work with physics
    public override void FixedUpdate() 
    {
        if (isInAttackRange())
        {
            Attack();
        } 
        else if (isInChaseRange())
        {
            Chase();
            //Debug.Log("Chase");
        } 
        else
        {
            Patrol();
        }

    }

    void Chase() //Adapted from [2] and [3] to use A* to make enemies chase the player
    {
        if (path == null) 
        {
            return;
        }

        //Checks if enemy has reached the end
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - RB2D.position).normalized;

        //Adds velocity to the enemy
        RB2D.velocity = direction * speed;

        enemyAnimator.SetFloat("Velocity_Y", Vector3.Normalize(RB2D.velocity).y);
        enemyAnimator.SetFloat("Vertical", direction.y > 0 ? 1 : -1);

        //Distance from next waypoint
        float distance = Vector2.Distance(RB2D.position, path.vectorPath[currentWaypoint]);

        //Checks if reached the current waypoint so it can move to the next one
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public virtual void Attack() {
        //Debug.Log("Attack");
    }
}
