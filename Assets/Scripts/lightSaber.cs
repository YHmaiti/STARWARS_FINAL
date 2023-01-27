using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSaber : MonoBehaviour
{
    private GameObject Saber;
    bool saberOn = false;
    private Vector3 saberLength;
    public OVRInput.Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        Saber = transform.Find("SingleLine-LightSaber").gameObject;
        saberLength = Saber.transform.localScale;
        Saber.transform.localScale = new Vector3(saberLength.x, 0, saberLength.z);
    }
    // Update is called once per frame
    void Update()
    {
        lightSaber_Power();
    }

    private void lightSaber_Power()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            if (Saber.transform.localScale.y == 0 && saberOn)
            {
                Saber.transform.localScale -= new Vector3(0, 0.0001f, 0);
                saberOn = false;
            }
            else
            {
                Saber.transform.localScale += new Vector3(0, 0.0001f, 0);
                saberOn = true;
            }
        }
    }
}
