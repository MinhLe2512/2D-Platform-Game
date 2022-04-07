using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    private float boundX = 0.15f;

    private void Start()
    {
        
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(lookAt.position.x, lookAt.position.y, transform.position.z);
    }
}
