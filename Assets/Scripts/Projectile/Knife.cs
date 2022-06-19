using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Projectile
{
    public float Speed = .6f;
    public int Passthrough = 1;
    public Vector3 Dir;
    public KnifeThrow Parant;

    private int _currentPassthroughCount;
    private float Distance = 0;
    private float _currentDistance;

    private void OnEnable()
    {
        if (Distance <= 0) Distance = 24;
        _currentPassthroughCount = 0;
        _currentDistance = 0;
    }
    
    public void Init(KnifeThrow knifeThrow, Vector2 dir, int dmg, float speed, int pass)
    {
        Parant = knifeThrow;
        Dir = dir;
        if (dir == Vector2.zero) Dir.x = 1;
        Damage = dmg;
        Speed = speed;
        Passthrough = pass;
        _currentDistance = 0;
        _currentPassthroughCount = 0;

        Vector3 offset = Vector3.Cross(Dir, Vector3.forward) * Random.Range(-0.15f, 0.15f);
        transform.position += offset;
        //transform.rotation = Quaternion.FromToRotation(Vector3.right, Dir);
        transform.right = Dir;
    }

    private void FixedUpdate()
    {
        transform.position += Dir * Speed * Time.fixedDeltaTime;
        _currentDistance += Speed * Time.fixedDeltaTime;
        if (_currentDistance > Distance)
        {
            Parant.ObjectPool.Destroy(gameObject);
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().Knockback();
            _currentPassthroughCount++;
            if(_currentPassthroughCount >= Passthrough)
            {
                Parant.ObjectPool.Destroy(gameObject);
            }
        }
    }
}
