using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bottle : Projectile
{
    public ParticleSystem ExplodeEfx;
    public float Speed = 5f;

    public HolyWater Parent { get; set; }
    public Vector3 Target { get; set; }

    private SpriteRenderer _sp;
    private bool _exploded = false;


    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _exploded = false;
        transform.right = (Target - transform.position).normalized;
    }

    private void Update()
    {
        if (_exploded) return;
        transform.position += Speed * Time.deltaTime * transform.right;

        if( Vector3.Distance(transform.position, Target) < 0.1f)
        {
            Explode();
        }
    }


    private void Explode()
    {
        _exploded = true;
        Parent.CreateArea(transform.position);
        _sp.enabled = false;
        ExplodeEfx.Play();

        Destroy(gameObject, 1f);
    }
}
