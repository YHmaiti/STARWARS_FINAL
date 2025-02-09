using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float growthRate = 0.1f;

    float psotionY;

    private void Start()
    {
        psotionY = transform.position.y;
    }

    private void Update()
    {
        // ensure the wave does not change y position across movement
        transform.position = new Vector3(transform.position.x, psotionY, transform.position.z);
    }

    void GrowWave()
    {
        transform.localScale += new Vector3(growthRate, 0f, growthRate);
        // move upsard with same rate so that the thing does not hit the ground
        transform.position += new Vector3(0, growthRate, 0);
    }

    void DestroyWave()
    {
        CancelInvoke("GrowWave"); // Stop growing
        gameObject.SetActive(false); // Deactivate instead of destroy if you reuse the object
    }
}