using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyObjs;
    private float nextSpawn;
    [SerializeField]
    public float maxSpawn;
    [SerializeField]
    public float minSpawn; 

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyObjs[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyObjs.Count == 0)
        {
            if (Time.time > nextSpawn)
            {
                Instantiate(enemyObjs[Random.Range(0, enemyObjs.Count)]);
                nextSpawn = Time.time + Random.Range(minSpawn, maxSpawn);
            }
        }

        else { }
            

    }
}
