using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cloneTrooper : MonoBehaviour
{
    // Creat enemy and make them shoot at the player
    public NavMeshAgent enemy;
    public Transform Player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);
        // if destination is reached then change animation
        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            animator.SetBool("isDead", true);
        }

    }
}
