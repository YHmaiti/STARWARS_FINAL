using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class lightSaber : MonoBehaviour
{
    private GameObject Saber;
    bool saberOn = false;
    private Vector3 saberLength;
    [SerializeField] InputActionReference AButton;
    [SerializeField] InputActionReference rightVelocity;
    private AudioSource audio;
    [SerializeField] AudioClip saberMovingSound;
    [SerializeField] AudioClip saberTurnOnSound;
    [SerializeField] AudioClip saberTurnOffSound;
    [SerializeField] AudioClip saberFightSound;
    [SerializeField] AudioClip saberNormalSound;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.spatialBlend = 1;
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
            audio.PlayOneShot(saberMovingSound);
        }
        else if (audio.isPlaying == false)
        {
            audio.PlayOneShot(saberNormalSound);
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
            Saber.transform.localScale += new Vector3(0, 0.01f, 0);
            Saber.SetActive(true);
            audio.PlayOneShot(saberTurnOnSound);
        }
        else if (Saber.transform.localScale.y > 0 && !saberOn)
        {
            Saber.transform.localScale -= new Vector3(0, 0.01f, 0);
            audio.PlayOneShot(saberTurnOffSound);
        }
        if (Saber.transform.localScale.y < 0 || Saber.transform.localScale.y==0)
        {
            Saber.SetActive(false);
        }
    }
}
