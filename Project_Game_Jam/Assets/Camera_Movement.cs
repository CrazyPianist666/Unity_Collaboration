
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;

    void Update()
    {


        transform.position = Player.position + offset;
    }

    
}
