using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgText : MonoBehaviour
{
    public float duration = 1.0f;
    public float flowuptime = 0.5f;
    public float flowupdist = 0.3f;

    private float _flowuptime = 0f;
    private float _duration = 0f;
    private float _startx;
    private float _flowy;

    private void OnEnable()
    {
        _flowuptime = flowuptime;
        _duration = duration;
        _startx = transform.position.x;
        _flowy = transform.position.y;
    }

    void Start()
    {
        _flowuptime = flowuptime;
        _duration = duration;
        _startx = transform.position.x;
        _flowy = transform.position.y;
    }

    void Update()
    {
        if(_flowuptime >= 0)
        {
            _flowuptime -= Time.deltaTime;
            float dist = (Time.deltaTime * flowupdist) / flowuptime;
            var pos = transform.position;
            _flowy = pos.y += dist;
            pos.x = _startx;
            transform.position = pos;

        }
        else if(_duration >= 0)
        {
            _duration -= Time.deltaTime;
            var pos = transform.position;
            pos.x = _startx;
            pos.y = _flowy;
            transform.position = pos;
        }
        else
        {
            _2DCanvas.Instance.ObjectPool.Destroy(gameObject);
        }
    }
}
