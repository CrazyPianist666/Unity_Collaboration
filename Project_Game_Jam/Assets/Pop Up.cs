using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject Panel;
    
    public bool isTrigger;
    
   
    void Start()
    {
        
        Panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isTrigger = true;
            Panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrigger = false;
            Panel.SetActive(false);
             
        }
    }

    
    void Update()
    {
       
    }

    
}
