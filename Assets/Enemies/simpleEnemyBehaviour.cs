using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemyBehaviour : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float health = 100;


    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.CompareTag("Weapon"))
        {
            Destroy(this.gameObject);
        }
    }
}
