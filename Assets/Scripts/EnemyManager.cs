using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;
    public float spawnTime1 = 1f;
    public float spawnTime2 = 0.5f;
    public float spawnTime3 = 0.2f;
    public int maxEnemies1 = 5;
    public int maxEnemies2 = 10;
    public int maxEnemies3 = 15;
    public int spawnLevel = 1;
    public bool spawnReady;
    public int counter = 0;
    public GameObject[] cloneCount;

    // Start is called before the first frame update
    private void Start()
    {
        spawnReady = true;
        // StartCoroutine(SpawnWaves());
    }

    private void spawnNewEnemy()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0f, spawnPoints.Length - 1));
        Instantiate(enemy, spawnPoints[randomNumber].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        // bool spawnReady = true;

        switch (spawnLevel)
        {
            case 1:
                {
                    cloneCount = GameObject.FindGameObjectsWithTag("Enemy");
                    //Debug.Log("Ready: " + cloneCount.Length);
                    if (counter != maxEnemies1 && spawnReady)
                    {
                        spawnNewEnemy();
                        counter++;
                        spawnReady = false;
                        StartCoroutine(spawnWait(spawnLevel));
                    }
                    if (counter == maxEnemies1 && cloneCount.Length == 0)
                    {
                        spawnLevel = 2;
                        counter = 0;
                        Debug.Log("Level 1 finished");
                    }
                }
                break;

            case 2:
                if (counter != maxEnemies2 && spawnReady)
                {
                    spawnNewEnemy();
                    counter++;
                    spawnReady = false;
                    StartCoroutine(spawnWait(spawnLevel));
                }
                if (counter == maxEnemies2 && cloneCount.Length == 0)
                {
                    spawnLevel = 3;
                    counter = 0;
                    Debug.Log("Level 2 finished");
                }
                break;

            case 3:
                if (counter != maxEnemies3 && spawnReady)
                {
                    spawnNewEnemy();
                    counter++;
                    spawnReady = false;
                    StartCoroutine(spawnWait(spawnLevel));
                }
                if (counter != maxEnemies3 && cloneCount.Length == 0)
                {
                    Debug.Log("You have Survived!");
                }
                break;

            default:
                break;
        }
    }

    private IEnumerator spawnWait(int level)
    {
        switch (level)
        {
            case 1:
                yield return new WaitForSeconds(spawnTime1);
                spawnReady = true;
                break;

            case 2:
                yield return new WaitForSeconds(spawnTime2);
                spawnReady = true;
                break;

            case 3:
                yield return new WaitForSeconds(spawnTime3);
                spawnReady = true;
                break;

            default:
                break;
        }
    }
}