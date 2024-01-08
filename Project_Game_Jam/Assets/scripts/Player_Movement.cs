
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float Movement_force = 1000f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0, Movement_force * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -Movement_force * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Movement_force * Time.deltaTime, 0, 0 );
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Movement_force * Time.deltaTime, 0, 0);
        }

    }
}
