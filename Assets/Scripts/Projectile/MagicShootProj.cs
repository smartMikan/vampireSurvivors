using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShootProj : Projectile
{
    public float Speed = .6f;
    public MagicShoot Parant;

    private Rigidbody2D _rb;
    private float _reserveDistance;
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
        _reserveDistance = 25f;
    }

    public void Init(MagicShoot parent, int dmg, float spd, Vector2 dir, Vector2 vdir, float maxdis = 25f)
    {
        Parant = parent;
        _lastPos = transform.position = parent.transform.position;
        Damage = dmg;
        Speed = spd;
        _localDirection = dir;
        _reserveDistance = maxdis;
        transform.right = vdir;
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

        _rb.MovePosition(transform.position + Speed * Time.fixedDeltaTime * _localDirection);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Parant.ObjectPool.Destroy(gameObject);
        }
    }
}
