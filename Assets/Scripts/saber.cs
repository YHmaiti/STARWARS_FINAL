using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    [SerializeField] AudioClip block;
    [SerializeField] AudioClip hit;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            other.GetComponent<ShotBehavior>().reflected = true;
            GetComponent<AudioSource>().PlayOneShot(block);
            //other.transform.position = Vector3.MoveTowards(transform.position, other.GetComponent<ShotBehavior>().originalEnemy.position, other.GetComponent<ShotBehavior>().step);
            // Debug.Log("Just hit back a bullet");
            /*Vector3 direction = ((other.transform.position + other.transform.forward) - other.transform.position).normalized;
            Vector3 inverse = direction * -1;
            Vector3 position = other.transform.position;
            other.transform.rotation = Quaternion.LookRotation(inverse);
            other.GetComponent<Rigidbody>().velocity *= -1;*/
        }
    }
}
