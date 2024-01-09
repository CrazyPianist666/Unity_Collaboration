using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWaypoint : MonoBehaviour
{
    public Transform Target;
    public float RotationSpeed;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Target.position-transform.position),RotationSpeed*Time.deltaTime);
        Vector3 tempRot = transform.localEulerAngles;
        tempRot.z = 0;
        transform.localEulerAngles = tempRot;
    }
}
