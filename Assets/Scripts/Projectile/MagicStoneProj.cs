using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStoneProj : Projectile
{
    public float Speed = .6f;
    public float Duration = 0;
    public MagicStone Parant;
    public TrailRenderer TrailRenderer;

    private Rigidbody2D _rb;
    private float _currentDuration;
    private Vector3 _localDirection;
    private Vector3 _lastPos;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _localDirection = transform.right;
        _lastPos = transform.position;
        if (Duration <= 0) Duration = 5f;
        TrailRenderer.Clear();
        TrailRenderer.startColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void Init(MagicStone parent, int dmg, float spd, float dur, float range, Vector2 dir)
    {
        Parant = parent;
        _lastPos = transform.position = parent.transform.position;
        Damage = dmg;
        Speed = spd;
        Duration = dur;
        _currentDuration = 0;
        _localDirection = dir;
        transform.localScale.Set(range, range, 1);
    }

    private void FixedUpdate()
    {
        _currentDuration += Time.fixedDeltaTime;
        _lastPos = transform.position;
        if (_currentDuration > Duration)
        {
            Parant.ObjectPool.Destroy(gameObject);
            return;
        }

        var rect = Parant.Bound;
        var max = rect.max;
        var min = rect.min;
        if( _lastPos.x >= max.x)
        {
            _lastPos.x = max.x-0.1f;
            if (_localDirection.x > 0) _localDirection.x = -_localDirection.x;
        }
        else if (_lastPos.x <= min.x)
        {
            _lastPos.x = min.x+0.1f;
            if (_localDirection.x < 0) _localDirection.x = -_localDirection.x;
        }

        if (_lastPos.y >= max.y)
        {
            _lastPos.y = max.y-0.1f;
            if (_localDirection.y > 0) _localDirection.y = -_localDirection.y;
        }
        else if(_lastPos.y <= min.y)
        {
            _lastPos.y = min.y+0.1f;
            if(_localDirection.y < 0) _localDirection.y = -_localDirection.y;
        }

        transform.position = _lastPos;

        _rb.MovePosition(transform.position + Speed * Time.fixedDeltaTime * _localDirection);

    }
}
