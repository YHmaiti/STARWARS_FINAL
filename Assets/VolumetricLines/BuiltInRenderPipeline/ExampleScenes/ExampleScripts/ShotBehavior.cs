using System.Collections;
using UnityEngine;

public class ShotBehavior : MonoBehaviour
{
    private Vector3 playerPositionAsOfShot;
    public float speed;
    public Transform originalEnemy;
    public float step;
    public bool reflected = false;

    private Vector3 lastKnownEnemyPosition;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime(50));
    }

    private IEnumerator DestroyAfterTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    // On destroy, cancel the coroutine
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        // Check if object has been destroyed or not
        if (originalEnemy != null)
        {
            lastKnownEnemyPosition = originalEnemy.position;
        }

        step = speed * Time.deltaTime;

        if (reflected)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownEnemyPosition, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPositionAsOfShot, step);
        }
    }

    public void SetTarget(Transform player)
    {
        playerPositionAsOfShot = player.position;
        transform.forward = playerPositionAsOfShot - transform.position;
    }
}