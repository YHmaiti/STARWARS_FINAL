using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class rayShoot : MonoBehaviour
{
    public Transform target;
    private float shootTimeStamp;
    public GameObject projectile;
    public GameObject trooper;
    public float shootCooldown;
    public Transform midbody;
    public bool Alive;
    public bool firstShoot;
    [SerializeField] AudioClip Blaster;
    // Update is called once per frame
    private void Start()
    {
        shootTimeStamp = Time.time;
        Alive = true;
        firstShoot = true;
    }
    void Update()
    {
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
}
