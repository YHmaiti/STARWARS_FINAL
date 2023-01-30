using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class lightSaber : MonoBehaviour
{
    private GameObject Saber;
    bool saberOn = false;
    bool On=false;
    bool Hum = false;
    private Vector3 saberLength;
    [SerializeField] InputActionReference AButton;
    [SerializeField] InputActionReference rightVelocity;
   // private new AudioSource audio;
    [SerializeField] AudioClip saberMovingSound;
    [SerializeField] AudioClip saberTurnOnSound;
    [SerializeField] AudioClip saberTurnOffSound;
    // [SerializeField] AudioClip saberFightSound;
    [SerializeField] AudioClip saberNormalSound;
    
    // Start is called before the first frame update
    void Start()
    {
       // audio = gameObject.GetComponent<AudioSource>();
        //audio.spatialBlend = 1;
       // audio.volume = 0.5f;
        
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
        // if velocity greater than 6 then playsound
        if (rightVelocity.action.ReadValue<Vector3>().magnitude > 6)
        {
             GetComponent<AudioSource>().PlayOneShot(saberMovingSound);
        }
        else if (GetComponent<AudioSource>().isPlaying == false && On)
        {
            GetComponent<AudioSource>().PlayOneShot(saberNormalSound);
            Hum = true;
            //GetComponent<AudioSource>().Stop();
        }
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
            if (Hum == true)
            {
                GetComponent<AudioSource>().Stop();
                Hum = false;
            }
            Saber.transform.localScale += new Vector3(0, 0.01f, 0);
            Saber.SetActive(true);
            if (On==false)
            {
                GetComponent<AudioSource>().PlayOneShot(saberTurnOnSound);
                On = true;
            }
        }
        else if (Saber.transform.localScale.y > 0 && !saberOn)
        {
            if (Hum == true)
            {
                GetComponent<AudioSource>().Stop();
                Hum = false;
            }
            Saber.transform.localScale -= new Vector3(0, 0.01f, 0);
            if (On == true)
            {
                GetComponent<AudioSource>().PlayOneShot(saberTurnOffSound);
                On = false;
            }
        }
        if (Saber.transform.localScale.y < 0 || Saber.transform.localScale.y==0)
        {
            Saber.SetActive(false);
           
        }
    }
}
