using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayShoot : MonoBehaviour
{
    //public Transform target;
    public GameObject Player;

    //private float timeOfLastShot;
    public GameObject projectile;

    public GameObject trooper;

    //public float shootCooldown;
    public Transform midbody;

    public bool Alive;
    [SerializeField] private AudioClip Blaster;
    public GameObject Parent;

    private List<Transform> targets = new List<Transform>();

    // Update is called once per frame
    private void Start()
    {
        Alive = true;
        Player = GameObject.Find("Player_XR Origin");
        Transform This = this.gameObject.transform;
        Transform parent = FindParentWithTag(This, "Enemy");
        Parent = parent.gameObject;

        targets = new List<Transform> {
            Player.transform.GetChild(2).transform,
            Player.transform.GetChild(3).transform,
            Player.transform.GetChild(4).transform,
            Player.transform.GetChild(5).transform,
            Player.transform.GetChild(6).transform,
            Player.transform.GetChild(7).transform,
            Player.transform.GetChild(8).transform,
        };

        print(targets);
        StartCoroutine(ShootContinuouslyWhileAlive());
    }

    private void Update()
    {
        trooper = Parent;
        midbody = trooper.transform.GetChild(4).transform;
        Alive = trooper.GetComponent<cloneTrooper>().Alive;
    }

    private IEnumerator ShootContinuouslyWhileAlive()
    {
        while (Alive)
        {
            Shoot();
            GetComponent<AudioSource>().PlayOneShot(Blaster);
            //timeOfLastShot = Time.time + shootCooldown;
            var randomFloat = Random.Range(3f, 4.5f);
            yield return new WaitForSeconds(randomFloat);
        }
    }

    private void Shoot()
    {
        GameObject ray = GameObject.Instantiate(projectile, transform.position, transform.rotation) as GameObject;

        var target = targets[Random.Range(0, targets.Count)];

        ray.GetComponent<ShotBehavior>().SetTarget(target);
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
        return null;
    }
}