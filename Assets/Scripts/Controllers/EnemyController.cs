using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    // Start is called before the first frame update
    void Start() {
        speed = 1.5f;
    }

    // Update is called once per frame
    void Update() {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 4) {
            Chase();
        }
    }

    private void IsPlayerInRange() {
    } 

    private void Chase() {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
