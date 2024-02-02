using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimRange : MonoBehaviour
{
   
    static public bool AnimRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AnimRange = true;

        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AnimRange = false;

        }
        
    }
}
