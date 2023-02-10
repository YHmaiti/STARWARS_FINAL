using Oculus.Voice.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blasterRay : MonoBehaviour
{
    //[SerializeField] float damage = 10f;
    [SerializeField] float speed = 500;
   // public Transform playerPosition;
    Rigidbody rB;
    
    // Start is called before the first frame update
    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        Transform playerPosition = GameObject.Find("Player").transform;
        Vector3 direction = playerPosition.position - transform.position;
        rB.AddForce(direction * speed * Time.deltaTime);

    }
}
