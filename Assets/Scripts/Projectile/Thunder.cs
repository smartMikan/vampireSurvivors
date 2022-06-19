using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Projectile
{
    public LightCall Parent;
    public float Duration = 0.3f;
    public Animator Animator;
    private float _duration = 0.3f;

    private void OnEnable()
    {
        _duration = Duration;
        Animator.Play("Thunder2");
    }

    private void Update()
    {
        _duration -= Time.deltaTime;
        if (_duration < 0)
        {
            Parent.ObjectPool.Destroy(gameObject);
        }
    }
}
