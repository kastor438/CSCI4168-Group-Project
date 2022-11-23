using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    internal Animator enemyAnimator;

    public GameObject player;
    public float speed;
    public Transform[] patrolPath;
    public int currentPatrolPoint;
    public Transform currentGoal;
    public int collisionDamage = 5;

    public Rigidbody2D RB2D;
    
    // Start is called before the first frame update
    public virtual void Start() 
    {
        enemyAnimator = GetComponent<Animator>();
        player = GameManager.Instance.player;
        RB2D = GetComponent<Rigidbody2D>();

        if (speed == 0) 
        {
            speed = 100f;
        }

    }

    public virtual void Patrol() 
    {
        if (Vector3.Distance(transform.position, patrolPath[currentPatrolPoint].position) > .5f)
        {
            Vector3 goalVector = Vector3.MoveTowards(transform.position, patrolPath[currentPatrolPoint].position, speed * Time.deltaTime);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collided with " + collision.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterStats>().TakeDamage(collisionDamage);
        }
    }
}
