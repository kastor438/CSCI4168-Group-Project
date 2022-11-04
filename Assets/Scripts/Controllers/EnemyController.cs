using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float chaseDistance;

    private float distance;
    // Start is called before the first frame update
    void Start() {
        speed = 1.5f;

        if (chaseDistance == 0) 
        {
            chaseDistance = 5.5f;
        }
    }

    // Update is called once per frame
    void Update() {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (IsPlayerInRange()) {
            Chase();
        }
    }

    private bool IsPlayerInRange() {
        if (distance > chaseDistance) {
            return false;
        }
        return true;
    } 

    private void Chase() {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
