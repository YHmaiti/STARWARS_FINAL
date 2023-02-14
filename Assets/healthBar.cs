using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public GameObject Player;
    Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        //get slider
        healthSlider = GetComponent<Slider>();
        setMaxHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
        setHealth(Player.GetComponent<playerHP>().health);
    }
    public void setMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = 100;
        healthSlider.value = 100;
    }

    public void setHealth(int health)
    {
        healthSlider.value = health;
    }
}
