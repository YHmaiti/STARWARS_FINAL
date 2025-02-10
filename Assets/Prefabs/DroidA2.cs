using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidA2 : MonoBehaviour
{
    public Transform player;
    public float speed = 0.2f;
    public float stop_distance;
    //public AudioSource audioSource;
    //public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {

            // get audio source of this object and play it called i am your father
            //audioSource.PlayOneShot(clip);


            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // fix the rotation to look at the target player
            transform.LookAt(player);

            if (Vector3.Distance(transform.position, player.position) < 2f)
            {
                // get the run animatioon and stop the runing animation
                this.GetComponent<Animator>().StopPlayback();
                //speed = 0;
                // paly the slash animation
                //GetComponent<Animator>().Play("slash");
                Debug.Log("they are here");
                //transform.position = new Vector3(transform.position.x, transform.position.y, 2.0f);
            }
        }
    }
}
