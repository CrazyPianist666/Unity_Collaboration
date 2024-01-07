
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float forward_force = 1000f;
    void Update()
    {
        rb.AddForce(0, 0, forward_force * Time.deltaTime);
    }
}
