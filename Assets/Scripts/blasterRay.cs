using Oculus.Voice.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blasterRay : MonoBehaviour
{
    //[SerializeField] float damage = 10f;
    [SerializeField] private float speed = 500;

    // public Transform playerPosition;
    private Rigidbody rB;

    // Start is called before the first frame update
    private void Start()
    {
        Transform playerPosition = Camera.main.transform;
        transform.forward = playerPosition.position - transform.position;
        rB = GetComponent<Rigidbody>();
        Vector3 direction = playerPosition.position - transform.position;
        rB.AddForce(direction * speed * Time.deltaTime);
    }
}