using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowFireball : Projectile
{
    public const float PI2 = Mathf.PI * 2;

    public float radius = 2f;
    public float range = 1f;
    public float speed = 0.125f;
    public float rad = 0.0f;

    private float damageinterval = 0.5f;
    private float _interval = 0.0f;

    private Dictionary<EnemyController, float> _enemies = new Dictionary<EnemyController, float>();


    public void Init()
    {
        var scale = transform.localScale;
        scale.x = scale.y = scale.z = range;
        transform.localScale = scale;

        var pos = transform.localPosition;
        rad %= PI2;
        var x = radius * Mathf.Cos(rad);
        var y = radius * Mathf.Sin(rad);
        pos.x = x;
        pos.y = y;
        transform.localPosition = pos;

        //var ppos = transform.parent.position;
        //var gpos = transform.position;
        //gpos.x = ppos.x + x;
        //gpos.y = ppos.y + y;
        //transform.position = gpos;

        _interval = damageinterval;
    }

    private void FixedUpdate()
    {
        var pos = transform.localPosition;
        var r = speed * PI2 * Time.fixedDeltaTime;
        rad += r;
        rad %= PI2;
        var x = radius * Mathf.Cos(rad);
        var y = radius * Mathf.Sin(rad);

        pos.x = x;
        pos.y = y;
        transform.localPosition = pos;

        //var ppos = transform.parent.position;
        //var gpos = transform.position;
        //gpos.x = ppos.x + x;
        //gpos.y = ppos.y + y;
        //transform.position = gpos;

        //transform.RotateAround(transform.parent.position, Vector3.forward, 360 * Time.fixedDeltaTime * speed);
        CastDamage();

        foreach (var e in _enemies.Keys)
        {
            var time = _enemies[e];
            time -= _interval;
        }
        var itemsToRemove = _enemies.Where(f => f.Value <= 0f).ToArray();
        foreach (var item in itemsToRemove)
            _enemies.Remove(item.Key);

        //_interval += Time.fixedDeltaTime;
        //if (_interval >= damageinterval)
        //{
        //    _interval = 0.0f;
        //    CastDamage();
        //}
    }

    private void CastDamage()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, range * 0.1f, 1 << 7).Select(x => x.GetComponent<EnemyController>()).ToList();
        //enemies.Except(_enemies.Keys).All(t => {
        //    _enemies.Add(t, _interval);
        //    t.GiveDamage(Damage);
        //    return true; 
        //});
        foreach (var e in enemies)
        {
            if (_enemies.ContainsKey(e)) continue;
            _enemies.Add(e, _interval);
            e.GiveDamage(Damage);
        }
    }
}
