
//1st
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidAttack : MonoBehaviour
{
    public List<GameObject> droids;
    public List<Vector3> droidPositions;
    public GameObject target;
    //public GameObject attacker;
    //public Vector3 attackerPosition;
    public float speed = 0.2f;
    public GameObject attacker, attacker2, attacker3;

    void Start()
    {
        //// Initialize the positions of the droids
        //for (int i = 0; i < droids.Count; i++)
        //{
        //    droidPositions.Add(droids[i].transform.position);
        //}
        //attackerPosition = transform.position;
    }

    void Update()
    {
        StartCoroutine(ActivateWaves());
        //StartCoroutine(MovingDroid());

        //MoveDroid();
    }

    IEnumerator ActivateWaves()
    {
        attacker.SetActive(true);
        yield return new WaitForSeconds(5f);
        attacker2.SetActive(true);
        yield return new WaitForSeconds(5f);
        attacker3.SetActive(true);

        for(int i = 0;i < droids.Count; i++)
        {
            Vector3 startPosition = droidPositions[i];
            Vector3 targetGroundPosition = new Vector3(target.transform.position.x, startPosition.y, target.transform.position.z);

            droids[i].transform.position = Vector3.Lerp(startPosition, targetGroundPosition, speed * Time.deltaTime);
           
            droidPositions[i] = droids[i].transform.position;

            //check for distance between target and droid
            if (Vector3.Distance(target.transform.position, droids[i].transform.position) < 2.0f)
            {
                Debug.Log("They are here");
            }
        }

        yield return null;

    }
    //void MoveDroid()
    //{
    //    if (target == null) return; // Ensure target exists

    //    Vector3 targetPosition = new Vector3(target.transform.position.x, attacker.transform.position.y, target.transform.position.z); // Keep Y constant
    //    transform.position = Vector3.MoveTowards(attacker.transform.position, targetPosition, speed * Time.deltaTime);
    //}




    //IEnumerator MovingDroid()
    //{
    //    //Vector3 targetPosition = target.transform.position; // Get the target's position
    //    //Vector3 startPosition = transform.position; // Get the droid's original position
    //    //Vector3 targetGroundedPosition = new Vector3(targetPosition.x, startPosition.y, targetPosition.z); // Keep the y-coordinate constant
    //    //float progress = 0f;

    //    //while (progress < 1f) // Continue until progress reaches 1
    //    //{
    //    //    // Start progress at 0
    //    //    progress += speed * Time.deltaTime; // Increment progress
    //    //    transform.position = Vector3.Lerp(startPosition, targetGroundedPosition, progress);
    //    //    yield return null; // Wait for the next frame
    //    //}
    //    //attackerPosition = transform.position;


    //    //this will be for a list
    //    //for (int i = 0; i < droids.Count; i++)
    //    //{
    //    //    while (progress < 1f) // Continue until progress reaches 1
    //    //    {
    //    //        progress += speed * Time.deltaTime; // Increment progress
    //    //        droids[i].transform.position = Vector3.Lerp(startPosition, targetGroundedPosition, progress);
    //    //        yield return null; // Wait for the next frame
    //    //    }

    //    //    // Update the starting position for the next move
    //    //    droidPositions[i] = droids[i].transform.position;
    //    //}
    //}
}

