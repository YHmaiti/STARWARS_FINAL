using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    [SerializeField] AudioClip block;
    [SerializeField] AudioClip hit;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            other.GetComponent<ShotBehavior>().reflected = true;
            GetComponent<AudioSource>().PlayOneShot(block);
        }
    }
}
