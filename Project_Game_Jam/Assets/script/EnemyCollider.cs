using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public GameObject enemyref;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "sword")
        {
            other.GetComponent<EnemySystem>().TakeDamage(20);
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
