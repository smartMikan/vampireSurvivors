using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Projectile
{
    public float G = 24;
    public AxeThrow Parant;

    private int _pirece = 1;
    private float _rotspd = 360;
    private Rigidbody2D _rb;
    private float _reserveDistance;
    private Vector3 _localDirection;
    private Vector3 _lastPos;

    private float _afterimageinterval = 0.1f;
    private float _currenttime = 0.0f;
    private SpriteRenderer _sp;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnEnable()
    {
        _localDirection = transform.right;
        _lastPos = transform.position;
        _reserveDistance = 25f;
        _currenttime = 0.0f;
    }

    public void Init(AxeThrow parent, int dmg, Vector2 dir, Vector2 vdir, float range, int pir, float maxdis = 25f)
    {
        Parant = parent;
        _lastPos = transform.position = parent.transform.position;
        Damage = dmg;
        _localDirection = dir;
        _reserveDistance = maxdis;
        _pirece = pir;
        transform.right = vdir;
        transform.localScale = new Vector3(range,range,range);
        _rotspd = dir.x > 0 ? -_rotspd : _rotspd;
    }

    private void Update()
    {
        // Rotate
        transform.Rotate(new Vector3(0,0,_rotspd*Time.deltaTime));

        _currenttime += Time.deltaTime;
        if(_currenttime >= _afterimageinterval)
        {
            _currenttime = 0;
            var obj = Parant.SpriteAfterImagePool.Create(_sp);
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

        // Palabola
        _localDirection.y -= G * Time.fixedDeltaTime;

        _rb.MovePosition(transform.position + _localDirection * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _pirece--;
            if (_pirece <= 0) Parant.ObjectPool.Destroy(gameObject);
        }
    }
}
