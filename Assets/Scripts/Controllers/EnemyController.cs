using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public float speed = 100f;
    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D RB2D;
    // Start is called before the first frame update
    void Start() {
        player = GameManager.Instance.player;
        seeker = GetComponent<Seeker>();
        RB2D = GetComponent<Rigidbody2D>();

        //Specifies a method to be repeated
        InvokeRepeating("UpdatePath", 0f, .5f);

        if (speed == 0) 
        {
            speed = 100f;
        }
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - RB2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime; //Force applied to the enemy to make it move

        //Adds force to the enemy
        RB2D.AddForce(force);

        //Distance from next waypoint
        float distance = Vector2.Distance(RB2D.position, path.vectorPath[currentWaypoint]);

        //Checks if reached the current waypoint so it can move to the next one
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}
