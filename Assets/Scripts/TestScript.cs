using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject Ball;

    private void Start()
    {
        var center = new Vector3(0, 0, 0);
        var radius = 3;
        for (int i = 0; i < 360; i += 20)
        {
            var rad = i * Mathf.PI / 180;
            var x = center.x + radius * Mathf.Cos(rad);
            var y = center.y + radius * Mathf.Sin(rad);
            Ball.transform.position = new Vector3(x, y, center.z);
            Instantiate(Ball);
        }
    }
}
