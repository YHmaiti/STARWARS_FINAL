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
    public int maxEnemies1 = 10;
    public int maxEnemies2 = 20;
    public int maxEnemies3 = 30;
    public int spawnLevel = 1;
    public bool ready;
    public int counter = 0;
    // Start is called before the first frame update
    private void spawnNewEnemy()
    {
        Random.seed = (int)DateTime.Now.Ticks;
        int randomNumber = Mathf.RoundToInt(Random.Range(0f,spawnPoints.Length-1));
        Instantiate(enemy, spawnPoints[randomNumber].transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ready: " + ready);
        switch (spawnLevel)
        {
            case 1:
                {
                    if(counter != maxEnemies1 && ready)
                    {
                        spawnNewEnemy();
                        counter++;
                        ready = false;
                        StartCoroutine(spawnWait(spawnLevel));
                    }
                    if(counter == maxEnemies1)
                    {
                        spawnLevel = 2;
                        counter = 0;
                    }
                }
                break;
            case 2:
                if (counter != maxEnemies2 && ready)
                {
                    spawnNewEnemy();
                    counter++;
                    ready = false;
                    StartCoroutine(spawnWait(spawnLevel));
                }
                if (counter == maxEnemies2)
                {
                    spawnLevel = 3;
                    counter = 0;
                }
                break;
            case 3:
                if (counter != maxEnemies3 && ready)
                {
                    spawnNewEnemy();
                    counter++;
                    ready = false;
                    StartCoroutine(spawnWait(spawnLevel));
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
                ready = true;
                break;
            case 2:
                yield return new WaitForSeconds(spawnTime2);
                ready = true;
                break;
            case 3:
                yield return new WaitForSeconds(spawnTime3);
                ready = true;
                break;
            default:
                break;
        }
    }
}
