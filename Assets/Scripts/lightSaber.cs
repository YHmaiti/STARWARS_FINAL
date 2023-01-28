using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class lightSaber : MonoBehaviour
{
    private GameObject Saber;
    bool saberOn = false;
    private Vector3 saberLength;
    public InputActionReference AButton;
    // Start is called before the first frame update
    void Start()
    {
        // source = gameObject.GetComponent<AudioSource>();
        Saber = transform.Find("SingleLine-LightSaber").gameObject;
        saberLength = Saber.transform.localScale;
        Saber.transform.localScale = new Vector3(saberLength.x, 0, saberLength.z);
        Saber.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        powerButton();
        lightSaber_Power();
    }
    private void powerButton()
    {
        if (AButton.action.WasPressedThisFrame())
        {
            saberOn = !saberOn;
        }
    }
    private void lightSaber_Power()
    {
        if (Saber.transform.localScale.y < saberLength.y && saberOn)
        {
            Saber.transform.localScale += new Vector3(0, 0.01f, 0);
            Saber.SetActive(true);
        }
        else if (Saber.transform.localScale.y > 0 && !saberOn)
        {
            Saber.transform.localScale -= new Vector3(0, 0.01f, 0);
        }
        if (Saber.transform.localScale.y < 0 || Saber.transform.localScale.y==0)
        {
            Saber.SetActive(false);
        }
    }
}
