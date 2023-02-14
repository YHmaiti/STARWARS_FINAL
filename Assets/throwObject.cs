using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class throwObject : MonoBehaviour
{
    // When the player presses the xr controller trigger button, the object will be thrown
    public GameObject objectToThrow;
    public Transform Spawnplace;
    [SerializeField] InputActionReference leftTrigger;
    public Transform throwPoint;
    public float throwForce = 40;
    public bool isAvailable = true;
    public XRGrabInteractable yes;
    // Update is called once per frame
    private void Start()
    {
        throwPoint = GameObject.Find("LeftHand Controller").transform;
        Spawnplace = GameObject.Find("Stones").transform;
       // isAvailable = true;
    }
    private void Update()
    {
    }
    public void throwingObject()
    {
        objectToThrow.GetComponent<Rigidbody>().AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        objectToThrow.GetComponent<Rigidbody>().useGravity = true;
        //destroy gameobejct after a few seconds
        spawnStone();
        Destroy(this.gameObject, 5f);
       // isAvailable = false;
    }
    public void spawnStone()
    {
        //instantiate gameobject
        Instantiate(objectToThrow, Spawnplace.position, Spawnplace.rotation);
    }
}
