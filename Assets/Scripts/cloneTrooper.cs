using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class cloneTrooper : MonoBehaviour
{
    // Creat enemy and make them shoot at the player
    public NavMeshAgent Enemy;

    public Transform playerPosition;
    private Animator animator;
    private int HP = 30;
    public Transform target;
    [SerializeField] private int speed = 5;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip Death;
    public bool Alive;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetBool("isDead", false);
        animator.SetBool("isMoving", true);
        Alive = true;
        playerPosition = GameObject.Find("Player_XR Origin").transform;
        target = playerPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        playerPosition = GameObject.Find("Player_XR Origin").transform;
        target = playerPosition;
        enemyMovement();
        // if hp is 0 then destroy the enemy
        if (HP <= 0)
        {
            Dead();
        }
    }

    // check and see if there is trigger enter subtract from hp
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            bool reflect = other.GetComponent<ShotBehavior>().reflected;
            if (other.gameObject.tag == "Bullet" && reflect)
            {
                GetComponent<AudioSource>().PlayOneShot(hit);
                HP -= 10;
                // destroy gameobject
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Saber" || other.gameObject.tag == "Lightning" || other.gameObject.tag == "Stone")
        {
            Dead();
        }
        /* else if (other.gameObject.tag == "Stone")
         {
             HP = 0;
         }*/
    }

    //ON collision with stone
    /*    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Stone")
            {
                *//*GameObject.Find("Stone").GetComponent<throwObject>().spawnStone();
                Destroy(collision.gameObject);*//*
                Dead();
            }
        }*/

    // Enemy animation and position
    public void enemyMovement()
    {
        // transform.LookAt(playerPosition);
        Enemy.SetDestination(playerPosition.position);
        // if reached player then stop moving
        if (Vector3.Distance(transform.position, playerPosition.position) < 10f)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * speed);
    }

    public void Dead()
    {
        Alive = false;
        animator.SetBool("isDead", true);
        animator.SetBool("isMoving", false);
        Destroy(this.gameObject, 5);
    }
}