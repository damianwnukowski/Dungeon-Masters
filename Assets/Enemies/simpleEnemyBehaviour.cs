using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float health = 100;
    private float dmg = 8;
    private float cooldown = 0;
    public float attackSpeed = 0.1f;


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

        if (health <= 0)
        {
            Text txt =  GameObject.FindGameObjectsWithTag("Score")[0].GetComponent<Text>();
            int tmpscore = int.Parse(txt.text.Trim());
            tmpscore++;
            txt.text = tmpscore.ToString("D4");


            GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<SpawnEnemies>().count--;
            Destroy(this.gameObject);
        }
    }

    public void damage(float damage)
    {
        health -= damage;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log(col.collider.gameObject.tag);
        if (col.collider.gameObject.CompareTag("Player") && cooldown <= 0)
        {
            PlayerMovement s = col.collider.gameObject.GetComponent<PlayerMovement>();
            s.damage(dmg);
            cooldown = attackSpeed;
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
