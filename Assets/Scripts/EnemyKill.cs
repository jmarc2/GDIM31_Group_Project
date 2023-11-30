using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = GameObject.FindWithTag("Enemy");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(enemy);
        }
    }

}
