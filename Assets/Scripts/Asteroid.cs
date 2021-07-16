using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotatespeed = 20f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Spawn_Manager spawnmanager;
    //[SerializeField] private AudioSource audiosource;

    void Start()
    {
        spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        //audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotatespeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other);
            spawnmanager.StartSpawning();
            Destroy(this.gameObject);
        }
    }
}
