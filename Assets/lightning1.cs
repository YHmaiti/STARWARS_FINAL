using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
   // public GameObject lightningObject;
    public bool activate;
    public bool recharge = true;
    // Start is called before the first frame update
    /*void Start()
    {
        lightningObject.SetActive(false);
        activate = false;
        recharge = true;
    }*/

    // Update is called once per frame
   /* void Update()
    {
        // if active wait for 1 second then deactivate
        if (activate)
        {
            recharge = false;
            StartCoroutine(Deactivate());
        }
        if (recharge == false)
        {
            StartCoroutine(Recharge());
        }
    }
    private void OnEnable()
    {
        activate = true;
    }
*/
    private void OnDisable()
    {
        StartCoroutine(Recharge());
    }
    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(5);
        recharge = true;
    }

    
}
