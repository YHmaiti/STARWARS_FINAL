using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cloneTrooper : MonoBehaviour
{
    // Creat enemy and make them shoot at the player
    public NavMeshAgent Enemy;
    public Transform playerPosition;
    private Animator animator;
    public GameObject Blaster;
    public GameObject blasterRay;
    //public GameObject CloneTrooper;
    //public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetBool("isDead", false);
        animator.SetBool("isMoving", true);
        //Enemy = GetComponent<NavMeshAgent>();
        // rotate blaster to face player
       // blasterRay.transform.LookAt(playerPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPosition);
        Enemy.SetDestination(playerPosition.position);
        // if reached player then stop moving
        if (Vector3.Distance(transform.position, playerPosition.position) < 10f)
        {
            animator.SetBool("isMoving", false);
        }
    }
}
