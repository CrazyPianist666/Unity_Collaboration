using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public int EnemyHP = 100;
    public Animator animator;
    public GameObject enemyref;

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
        print("Take damage");
        if (EnemyHP > 0)
        {
            animator.SetTrigger("Damage");
        }

        if (EnemyHP <= 0)
        {
            animator.SetTrigger("Die");
            Object.Destroy(enemyref);
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
}
