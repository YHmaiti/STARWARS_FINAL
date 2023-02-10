using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {
	public Transform target;
	public float speed;
    public Transform originalEnemy;
    public float step;
    public bool reflected = false;
    //public GameObject trooper;
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () 
	{
        step = speed * Time.deltaTime;
        if (reflected == false)
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                Vector3 newLook = Vector3.RotateTowards(transform.forward, target.position, step, 0.0f);
                newLook.x = -90f;
                transform.rotation = Quaternion.LookRotation(newLook);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalEnemy.position, step);
        }
    }

    
    public void setTarget(Transform t)
    {
        target = t;
    }
}
