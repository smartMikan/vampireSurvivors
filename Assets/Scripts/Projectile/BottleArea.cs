using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleArea : DamageArea
{
    public Vector3 Dir;
    public float Distance;

    private float _duration;

    private void Awake()
    {
        buffType = BuffType.BottleDamage;
    }

    private void Update()
    {
        UpdateArea();

        _duration += Time.deltaTime;
        if (_duration > Duration)
        {
            Destroy(gameObject);
        }
    }
}
