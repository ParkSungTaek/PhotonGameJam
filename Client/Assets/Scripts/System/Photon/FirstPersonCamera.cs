using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Target.position + new Vector3(0.0f, 15.0f, -2.0f);
    }
}
