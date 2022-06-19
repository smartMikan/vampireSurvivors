using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAfterImageObject : MonoBehaviour
{
    private float _duration;
    private SpriteRenderer _sprenderer;
    private float _fadeoutdelta;
    private void Awake()
    {
        _sprenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector3 dir, Vector3 scale, float duration, Sprite sp, Color startcolor, float fadeouttime, bool flipx, bool flipy)
    {
        _duration = duration;
        _sprenderer.sprite = sp;
        _sprenderer.color = startcolor;
        _sprenderer.flipX = flipx;
        _sprenderer.flipY = flipy;

        _fadeoutdelta = 1.0f / fadeouttime;


        transform.right = dir;
        transform.localScale = scale;
    }

    
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        var color = _sprenderer.color;
        color.a -= _fadeoutdelta * Time.deltaTime;
        _sprenderer.color = color;

        _duration -= Time.deltaTime;
        if (_duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
