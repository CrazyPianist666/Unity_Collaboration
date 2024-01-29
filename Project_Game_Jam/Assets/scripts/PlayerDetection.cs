using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    static public bool PlayerOnRange = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            PlayerDetection.PlayerOnRange = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
