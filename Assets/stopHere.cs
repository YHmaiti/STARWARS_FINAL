using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stopHere : MonoBehaviour
{

    // write a transform 2 meters behind the user and another transform that store the same rotation of the gameobject at start
    public Transform stopPoint;
    public Transform startRotation;
    // Start is called before the first frame update
    void Start()
    {
        // set the position of the stopPoint 2 meters behind the user
        stopPoint.position = Camera.main.transform.position - Camera.main.transform.forward * 2;
        // set the rotation of the startRotation to the same rotation of the gameobject at start
        startRotation.rotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        // continuously keep the portal inactive and apply these transforms:

        //    portal.transform.position = new Vector3(0, 0.478f, 0);
        //    portal.transform.eulerAngles = new Vector3(0, 180, 0);
        //    portal.transform.localScale = new Vector3(100, 100, 100);

        transform.position = new Vector3(0, 0.478f, 0);
        transform.eulerAngles = new Vector3(0, 180, 0);
        transform.localScale = new Vector3(100, 100, 100);



    }
}
