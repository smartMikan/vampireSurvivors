using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Projectile
{
    public TrailRenderer TrailRenderer;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        TrailRenderer.startColor = _spriteRenderer.material.GetColor("_GlowColor");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.LogWarning("KNOK BACK!");
            collision.GetComponent<EnemyController>().Knockback();
        }
    }
}
