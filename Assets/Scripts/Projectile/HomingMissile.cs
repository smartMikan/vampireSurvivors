using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingMissile : Projectile
{
    public TrailRenderer TrailRenderer;
    public ObjectPool Pool { get; set; }
    public ObjectPool ExplodePool { get; set; }
    public Vector3 Target { get => target; set => target = value; }
    public float Speed { get => speed; set => speed = value; }
    public float RotSpeed { get => rotSpeed; set => rotSpeed = value; }
    public float Radius { get => radius; set => radius = value; }

    private float radius;

    private float rotSpeed;

    private float speed;

    private Vector3 target;

    private const float forcedirtime = 0.5f;
    private float dirtime = 0.0f;
    private float explodedis = 0.1f;

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        TrailRenderer.Clear();
    }

    public void Init(ObjectPool pool, ObjectPool explodepool, int dmg, float exploderadius, float speed, float rotspeed, Vector3 startpos, Vector3 target)
    {
        Pool = pool;
        ExplodePool = explodepool;
        Speed = speed;
        RotSpeed = rotspeed;
        Target = target;
        Damage = dmg;
        Radius = exploderadius;

        transform.position = startpos;
        Vector3 dir = target - startpos;
        var r = Random.Range(0, 2);
        if( r > 0) transform.up = dir;
        else transform.up = -dir;

        dirtime = 0.0f;

        explodedis = Speed * Time.fixedDeltaTime;

    }


    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target);
        if(distance < explodedis)
        {
            Explode();
        }
    }

    private void FixedUpdate()
    {
        dirtime += Time.fixedDeltaTime;
        Vector3 dir = target - transform.position;
        dir.Normalize();
        float rotAmount = Vector3.Cross(dir, transform.right).z;
        _rb.angularVelocity = -rotAmount * RotSpeed;
        if (dirtime >= forcedirtime)
        {
            //RotSpeed *= 2.0f;
            transform.right = Vector3.Slerp(transform.right, dir,0.5f);
        }
        _rb.velocity = transform.right * speed;
    }

    private void Explode()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, Radius, 1 << 7).All( t => {
            var enemy = t.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.GiveDamage(Damage);
            }
            return true; 
        });

        ExplodePool.Instantiate(transform.position);
        Pool.Destroy(gameObject);

    }
}
