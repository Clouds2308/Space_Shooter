using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Spawn_Manager spawnmanager;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleshot;
    [SerializeField] private bool isspeed = false;
    [SerializeField] private bool istripleshot = false;
    [SerializeField] private bool isshield = false;
    [SerializeField] private float speed = 3.5f;
    private float speedmultiplier = 2f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int lives = 3;
    private float canFire = -1f;
    [SerializeField] GameObject shield;
    [SerializeField] private int score;
    private UIManager uimanager;
    [SerializeField] private GameObject leftfire;
    [SerializeField] private GameObject Rightfire;
    [SerializeField]private AudioClip lasersound;
    private AudioSource audiosource;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        //spawnmanager = gameObject.GetComponent<Spawn_Manager>();
        spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audiosource = GetComponent<AudioSource>();

        if (audiosource == null)
        {
            Debug.LogError("Audio Source on the player is Null");
        }

        if (spawnmanager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }

        if (uimanager ==  null)
        {
            Debug.LogError("UI Manager is Null");
        }
        else
        {
            audiosource.clip = lasersound;
        }
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            Shoot();
        }
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (isspeed == true)
        {
            transform.Translate(direction * speed * speedmultiplier * Time.deltaTime);
        }
        else if (isspeed == false)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (transform.position.x > 11.35f)
        {
            transform.position = new Vector3(-11.35f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.35f)
        {
            transform.position = new Vector3(11.35f, transform.position.y, 0);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.96f, 5.96f), 0);
    }

    void Shoot()
    {
        canFire = Time.time + fireRate;

        if (istripleshot == true)
        {
            Instantiate(tripleshot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }
        audiosource.Play();
    }

    public void Damage()
    {
        if (isshield == true)
        {
            isshield = false;
            shield.SetActive(isshield);
            return;
        }
        lives--;
        uimanager.UpdateLives(lives);

        if (lives == 2)
        {
            leftfire.SetActive(true);
        }
        else if (lives == 1)
        {
            Rightfire.SetActive(true);
        }

        if (lives < 1)
        {
            Debug.Log("Dead");
            spawnmanager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        istripleshot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
            yield return new WaitForSeconds(5f);
            istripleshot = false;
    }

    public void SpeedActive()
    {
        isspeed = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isspeed = false;
    }

    public void ShieldActive()
    {
        isshield = true;
        shield.SetActive(isshield);
    }

    public void AddScore(int points)
    {
        score += points;
        uimanager.UpdateScore(score);
    }
}
