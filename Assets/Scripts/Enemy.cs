using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private GameObject laser;
    private Player player;
    private Animator anim;
    [SerializeField] private AudioSource audiosource;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audiosource = GetComponent<AudioSource>();

        if (audiosource == null)
        {
            Debug.LogError("Audio Source on the enemy is Null");
        }

        if (player == null)
        {
            Debug.LogError("Player is Null");
        }
        anim = GetComponent<Animator>();
        if (anim ==  null)
        {
            Debug.LogError("Animator is Null");
        }
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float randx = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(randx, 7.7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: "+other.name+" Tag: "+other.tag);

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (player != null)
            {
                player.AddScore(10);
            }
            anim.SetTrigger("onEnemyDeath");
            speed = 0;
            audiosource.Play();
            Destroy(this.gameObject,2.7f);
        }

        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            anim.SetTrigger("onEnemyDeath");
            speed = 0;
            audiosource.Play();
            Destroy(this.gameObject,2.7f);
        }

    }
}
