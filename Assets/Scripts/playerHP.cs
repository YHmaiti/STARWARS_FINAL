using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHP : MonoBehaviour
{
    public int health = 100;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= 10;
            Debug.Log("Health: " + health);
            //destroy other gameobject
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("You died");
        }
    }
}
