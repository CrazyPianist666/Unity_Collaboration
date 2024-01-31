using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    public float maxHealth = 100f;
    public Slider easeHealthSlider;
    public float health = 0;
    private float lerpSpeed = 0.05f;
    void Start()
    {
        health = maxHealth;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (healthBarSlider.value != health)
        {
            healthBarSlider.value = health;
        }
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            takeDamage(10);
        
        }
        if(healthBarSlider.value != easeHealthSlider.value) 
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        
        
        }

        
        
        
    }

    void takeDamage(float damage)
    {
        health -= damage;

    }
}
