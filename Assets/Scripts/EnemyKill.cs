using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private AudioSource frog;
    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    private float newTime;
    // Start is called before the first frame update
    void Start()
    {
        float[] croak = new float[maxTime];
        frog = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        frog = GetComponent<AudioSource>();

        if (Time.time > newTime) 
        {
            newTime = Time.time * Random.Range(minTime, maxTime);
            frog.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject.GetComponentInParent<Enemy>().gameObject);
        }

    }


}
