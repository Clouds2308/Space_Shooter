using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // 0 = triple shot
    // 1 = speed
    // 2 = shields
    [SerializeField] private float speed = 3f;
    [SerializeField] private int powerupID;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        {
                            player.TripleShotActive();
                            break;
                        }
                    case 1:
                        {
                            player.SpeedActive();
                            break;
                        }
                    case 2:
                        {
                            player.ShieldActive();
                            break;
                        }
                    default:
                        {
                            Debug.Log("Default Case");
                            break;
                        }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
