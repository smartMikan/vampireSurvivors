using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public float Speed = .6f;
    public int ReboundCount = 1;
    public int Passthrough = 1;
    public float Distance = 0;
    public Skill Parant;
    public TrailRenderer TrailRenderer;

    private Rigidbody2D _rb;
    private Vector3 _localDirection;
    private string _blockTag = "Block";
    private string _enemyTag = "Enemy";
    private float _currentDistance;
    private int _currentReboundCount;
    private int _currentPassthroughCount;
    private Vector3 _lastPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _currentDistance = 0;
        _currentReboundCount = 0;
        _currentPassthroughCount = 0;
        _localDirection = transform.right;
        _lastPos = transform.position;
        if (Distance <= 0) Distance = 24;
        TrailRenderer.Clear();
        TrailRenderer.startColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private void FixedUpdate()
    {
        _currentDistance += Vector3.Distance(transform.position, _lastPos);
        _lastPos = transform.position;
        if (_currentDistance > Distance)
        {
            Parant.ObjectPool.Destroy(gameObject);
            return;
        }

        _rb.MovePosition(transform.position + _localDirection * Speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_blockTag))
        {
            if (_currentReboundCount < ReboundCount)
            {
                _currentReboundCount++;

                var hit = Physics2D.Raycast(transform.position, transform.right);
                _localDirection = Vector2.Reflect(transform.right, hit.normal);
            }
            else
            {
                Parant.ObjectPool.Destroy(gameObject);
            }
        }
        else if (collision.CompareTag(_enemyTag))
        {
            if (_currentPassthroughCount >= Passthrough)
            {
                Parant.ObjectPool.Destroy(gameObject);
            }
            else
            {
                _currentPassthroughCount++;
            }
        }
    }
}
