using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float speed = 100f;
    public Transform[] patrolPath;
    public int currentPatrolPoint;
    public Transform currentGoal;

    public Rigidbody2D RB2D;
    
    // Start is called before the first frame update
    public virtual void Start() 
    {
        player = GameManager.Instance.player;
        RB2D = GetComponent<Rigidbody2D>();

        if (speed == 0) 
        {
            speed = 100f;
        }

    }

    public virtual void Patrol() 
    {
        Debug.Log(patrolPath[currentPatrolPoint]);
        if (Vector3.Distance(transform.position, patrolPath[currentPatrolPoint].position) > .01f)
        {
            Vector3 goalVector = Vector3.MoveTowards(transform.position, patrolPath[currentPatrolPoint].position, speed/50 * Time.deltaTime);
            RB2D.MovePosition(goalVector);
        }
        else
        {
            MoveGoal();
        }
        
    }

    public void MoveGoal()
    {
        if (currentPatrolPoint == patrolPath.Length - 1)
        {
            currentPatrolPoint = 0;
            currentGoal = patrolPath[0];
        }
        else
        {
            currentPatrolPoint++;
            currentGoal = patrolPath[currentPatrolPoint];
        }
    }

    public virtual void FixedUpdate()
    {
        Patrol();
    }
}
