using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Collider>().enabled = false;
    }
}
