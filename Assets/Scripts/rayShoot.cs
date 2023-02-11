using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class rayShoot : MonoBehaviour
{
    public Transform target;
    public GameObject Player;
    private float shootTimeStamp;
    public GameObject projectile;
    public GameObject trooper;
    public float shootCooldown;
    public Transform midbody;
    public bool Alive;
    public bool firstShoot;
    [SerializeField] AudioClip Blaster;
    public GameObject Parent;
    // Update is called once per frame
    private void Start()
    {
        shootTimeStamp = Time.time;
        Alive = true;
        firstShoot = true;
        Player = GameObject.Find("Player_XR Origin");
        Transform This = this.gameObject.transform;
        Transform parent = FindParentWithTag(This, "Enemy");
        Parent = parent.gameObject;
    }
    void Update()
    {
        trooper = Parent;
        // get shootting are from player
        target = Player.transform.GetChild(2).transform;
        //projectile = GameObject.Find("Bullet");
        midbody = trooper.transform.GetChild(4).transform;
        Alive = trooper.GetComponent<cloneTrooper>().Alive;
        if (Alive)
        {
            if (firstShoot)
            {
                StartCoroutine(ShootWait());
            }
            else if (Time.time > shootTimeStamp && firstShoot == false)
            {
                shoot();
                GetComponent<AudioSource>().PlayOneShot(Blaster);
                shootTimeStamp = Time.time + shootCooldown;
            }
        }
    }

    private IEnumerator ShootWait()
    {
        yield return new WaitForSeconds(1);
        firstShoot = false;
    }


    void shoot()
    {
        GameObject ray = GameObject.Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        ray.GetComponent<ShotBehavior>().setTarget(target);
        // StartCoroutine(ShootCooldown());
        ray.GetComponent<ShotBehavior>().originalEnemy = midbody;
    }

    public Transform FindParentWithTag(Transform child, string tag)
    {
        Transform next = child;
        while (next.parent != null)
        {
            if (next.parent.tag == tag)
            {
                return next.parent;//found ancestor with tag
            }
            next = next.parent.transform;
        }
        //search failed
        return null;
    }
}
