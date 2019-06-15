using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float speedModifier;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Vector3 position;
    private float health= 100;
    public Sprite full;
    public Sprite half;
    public Sprite empty;


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

        UpdateHealth();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    private void UpdateHealth() {
        GameObject[] healths = GameObject.FindGameObjectsWithTag("Heart");


        healths[0].GetComponent<SpriteRenderer>().sprite = health <= 12.5 ? health <= 0 ? empty : half : full;
        healths[1].GetComponent<SpriteRenderer>().sprite = health <= 37.5 ? health <= 25 ? empty : half : full;
        healths[2].GetComponent<SpriteRenderer>().sprite = health <= 62.5 ? health <= 50 ? empty : half : full;
        healths[3].GetComponent<SpriteRenderer>().sprite = health <= 87.5 ? health <= 75  ? empty : half : full;

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

    public void damage(float damage)
    {
        health -= damage;
    }
}
