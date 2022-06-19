using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    public float Rotspeed { get; set; }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Rotspeed * Time.deltaTime));
    }
}
