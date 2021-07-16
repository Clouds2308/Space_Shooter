using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemycontainer;
    [SerializeField] private GameObject[] powerups;

    private bool stopspawn = false;
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while(stopspawn == false)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
            newEnemy.transform.parent = enemycontainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(6f);

        while (stopspawn == false)
        {
            Vector3 pos = new Vector3(Random.Range(-7.5f, 7.5f), 6, 0);
            int randomPowerUp = Random.Range(0,3);
            Instantiate(powerups[randomPowerUp], pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void onPlayerDeath()
    {
        stopspawn = true;
    }
}
