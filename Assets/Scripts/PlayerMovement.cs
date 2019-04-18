using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float speedModifier;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Vector3 position;
    
    // Start is called before the first frame update
    void Start() {     
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        change.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
            speedModifier = 2;
        else
            speedModifier = 1;
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(
            transform.position + change * speed * speedModifier * Time.deltaTime
        );

        //rotate based on moving direction, don't change if moving vertically
        if (change.x < 0)
            transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1));
        else if(change.x > 0)
            transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1));
    }
}
