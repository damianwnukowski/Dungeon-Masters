using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float attackSpeed;

    float cooldownTimer = 0;

    PlayerMovement player;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            int direction = 1;
            if(transform.parent.transform.rotation.y == 0){
                direction = -1;
            }
            transform.rotation = Quaternion.AngleAxis(direction * 90 * (((attackSpeed - cooldownTimer)/attackSpeed)), new Vector3(0, 0,1));
        }
        else
        {
            cooldownTimer = 0;
            transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0,1));

        }
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer == 0)
        {
            cooldownTimer = attackSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.collider.gameObject.tag);
        if (col.collider.gameObject.CompareTag("Enemy"))
        {
            SimpleEnemyBehaviour s = col.collider.gameObject.GetComponent<SimpleEnemyBehaviour>();
            if (cooldownTimer > 0)
                s.damage(damage);
        }
    }
}
