using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order66 : MonoBehaviour
{
    [SerializeField] AudioClip order66;
    [SerializeField] AudioClip themeSong;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(order66);
    }
    void Update()
    {
        //if audio source is not playing then play theme song
        if (GetComponent<AudioSource>().isPlaying == false)
        {
            // lower the volume before playing theme song
            GetComponent<AudioSource>().volume = 0.4f;
            GetComponent<AudioSource>().PlayOneShot(themeSong);
        }
    }
}
