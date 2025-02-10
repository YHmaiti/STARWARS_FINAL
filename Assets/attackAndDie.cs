using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackAndDie : MonoBehaviour
{
    public GameObject target;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        target = GameObject.Find("Darth2");
        if (target != null && target.activeSelf)
        {

            if (Vector3.Distance(transform.position, target.transform.position) < 2f)
            {

                // get the script from the target DroidA2 and make the speed 0
                target.GetComponent<DroidA2>().speed = 0;
                this.GetComponent<DroidA2>().speed = 0;
                animator.Play("slash");
                //transform.position = new Vector3(transform.position.x, transform.position.y, 2.0f);

                // paly the animation on the target object called dieVador
                target.GetComponent<Animator>().Play("dieVador");
                // destroy the target object after 2 seconds
                Destroy(target, 2);
                // go over all the clone troops objects and make them sdisappear
                GameObject[] clones = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject clone in clones)
                {
                    clone.SetActive(false);
                    Destroy(clone);
                }

                animator.Play("win");
            }
        }
    }
}
