using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public float spawnCooldown = 1000f;
    private float cooldownLeft = 0;
    public int count = 0;

    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        cooldownLeft = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownLeft > 0 || count >= 100)
        {
            cooldownLeft -= Time.deltaTime;
        }
        else
        {
            count++;
            prefab = GameObject.FindGameObjectWithTag("Enemy");
            cooldownLeft = spawnCooldown;
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Respawn");
            int rand = Random.Range(0, spawners.Length - 1);
            Debug.Log("spawnowanko");
            Debug.Log(spawners.Length);
            GameObject obj = Instantiate(prefab);
            obj.transform.position = spawners[rand].transform.position;

        }
    }
}
