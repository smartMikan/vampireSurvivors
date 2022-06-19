using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : Projectile
{
    public float duration = 0.2f;
    public float radius = 1f;
    public bool flip = false;
    public float offsetY = 0.0f;
    
    
    private float _duration = 0.0f;

    public void Init()
    {
        // init pos
        transform.position = transform.parent.position;
        var scale = transform.localScale;

        // flip
        scale.y = scale.z = radius;
        scale.x = flip ? -radius : radius;
        transform.localScale = scale;

        // reset duration
        _duration = 0.0f;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        _duration += Time.deltaTime;
        if(_duration > duration)
        {
            gameObject.SetActive(false);
        }

        // update pos cause we have rigid boby
        var position = transform.parent.position;
        position.y += offsetY;
        transform.position = position;
    }


}
