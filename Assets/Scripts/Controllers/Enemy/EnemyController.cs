using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    internal Animator enemyAnimator;

    internal GameObject player;
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
        speed = GetComponent<EnemyStats>().enemy.movementSpeed;

        if (speed == 0) 
        {
            speed = 3f;
        }

    }

    //Patrolling function adapted from [1] 
    public virtual void Patrol() 
    {
        if (Vector3.Distance(transform.position, patrolPath[currentPatrolPoint].position) > .5f)
        {
            Vector3 goalVector = Vector3.MoveTowards(transform.position, patrolPath[currentPatrolPoint].position, speed * Time.deltaTime);
            enemyAnimator.SetFloat("Vertical", (patrolPath[currentPatrolPoint].position.y - transform.position.y) > 0 ? 1 : -1);
            enemyAnimator.SetFloat("Velocity_Y", Vector3.Normalize(patrolPath[currentPatrolPoint].position - transform.position).y);
            RB2D.MovePosition(goalVector);
        }
        else
        {
            MoveGoal();
        }
        
    }

    //Patrolling function adapted from [1] 
    public void MoveGoal()
    {
        if (currentPatrolPoint == patrolPath.Length - 1) //Checks if character is at the end of the array
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterStats>().TakeDamage(collisionDamage);
        }
    }
}
