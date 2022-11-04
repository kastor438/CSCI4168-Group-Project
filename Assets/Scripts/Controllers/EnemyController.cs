using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 100f;
    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start() {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        //Specifies a method to be repeated
        InvokeRepeating("UpdatePath", 0f, .5f);

        if (speed == 0) 
        {
            speed = 100f;
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) 
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) //Checks if seeker is currently calculating a path
        {
            seeker.StartPath(rigidbody.position, player.position, OnPathComplete);
        }
    }

//Called a fixed number of time per seconds to properly work with physics
    void FixedUpdate() {
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime; //Force applied to the enemy to make it move

        //Adds force to the enemy
        rigidbody.AddForce(force);

        //Distance from next waypoint
        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);

        //Checks if reached the current waypoint so it can move to the next one
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}
