using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
  
    static public bool playerfound = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerfound = true;
            
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerfound=false;
        }
    }

}
