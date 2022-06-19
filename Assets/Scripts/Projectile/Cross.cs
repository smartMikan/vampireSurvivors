using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : Projectile
{
    public float Speed = 2f;
    public SpriteRenderer CrossSp;
    public CrossThrow Parant;

    private Rigidbody2D _rb;
    private float _reserveDistance;
    private Vector3 _localDirection;
    private Vector3 _lastPos;
    private float _rotspd = 540;
    private float _afterimageinterval = 0.1f;
    private float _currenttime = 0.0f;
    private float _speeddec = 1f;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _localDirection = transform.right;
        _lastPos = transform.position;
        _reserveDistance = 100f;
    }

    public void Init(CrossThrow parent, int dmg, float spd, Vector2 dir, float range, float maxdis = 100f)
    {
        Parant = parent;
        _lastPos = transform.position = parent.transform.position;
        Damage = dmg;
        Speed = spd;
        _localDirection = dir;
        _reserveDistance = maxdis;
        transform.right = dir;
        transform.localScale = new Vector3(range, range, range);
        _rotspd = dir.x > 0 ? -_rotspd : _rotspd;
        _speeddec = Speed*2;
    }

    private void Update()
    {
        // Rotate
        CrossSp.transform.Rotate(new Vector3(0, 0, _rotspd * Time.deltaTime));

        _currenttime += Time.deltaTime;
        if (_currenttime >= _afterimageinterval)
        {
            _currenttime = 0;
            var obj = Parant.SpriteAfterImagePool.Create(CrossSp);
            var sr = obj.GetComponent<SelfRotate>();
            if (!sr)
            {
                sr = obj.AddComponent<SelfRotate>();
                sr.Rotspeed = _rotspd;
            }
            else
            {
                sr.Rotspeed = _rotspd;
            }
        }
    }

    private void FixedUpdate()
    {
        _reserveDistance -= Vector3.Distance(transform.position, _lastPos);
        _lastPos = transform.position;
        if (_reserveDistance < 0f)
        {
            Parant.ObjectPool.Destroy(gameObject);
            return;
        }

        _rb.MovePosition(transform.position + _localDirection * Speed * Time.fixedDeltaTime);

        Speed -= _speeddec * Time.fixedDeltaTime;
        _speeddec += Time.fixedDeltaTime;
    }
}
