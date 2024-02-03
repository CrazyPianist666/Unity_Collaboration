using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public Slider HealthSlider;
    public int MaxHealth;
    int CurrentHealth;
    int Cash;
    public TextMeshProUGUI CashText;
    void Start()
    {
        SetDefs();
    }

    void SetDefs()
    {
        Cash = 20;
        CashText.text = Cash + "";
        CurrentHealth = PlayerPrefs.GetInt("Health", 0);
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = CurrentHealth;
    }

    public void BuyHealth(int Price)
    {
        
        if(CurrentHealth < MaxHealth) 
        {
            if (Cash > Price)
            {
                Cash -= Price;
                CashText.text = Cash+ "";
                CurrentHealth += 10;
                PlayerPrefs.SetInt("Health", CurrentHealth);
                HealthSlider.value = CurrentHealth;
                Debug.Log("Health Upgraded");
            }
            else
            {
                Debug.Log("Out Of Cash");
            }
            

        }
        else
        {
            Debug.Log("Health Full");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
