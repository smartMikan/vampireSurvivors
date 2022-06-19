using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageArea : Projectile
{
    public float Radius = 6;
    public float Interval = .1f;
    public float Duration = .1f;
    public Transform Area;
    protected BuffType buffType;
    protected Dictionary<EnemyController, float> _enemies = new Dictionary<EnemyController, float>();

    private void Update()
    {
        UpdateArea();
    }

    protected void UpdateArea()
    {
        if (Area.localScale.x != Radius)
        {
            Area.localScale = Vector3.one * Radius;
        }

        var enemies = Physics2D.OverlapCircleAll(transform.position, Radius * 0.5f, 1 << 7).Select(x => x.GetComponent<EnemyController>()).ToList();
        var except = _enemies.Keys.Except(enemies);
        foreach (var e in except)
        {
            _enemies.Remove(e);
        }

        foreach (var e in enemies)
        {
            var buff = e.Buff.Where(x => x.Type == buffType).FirstOrDefault();
            if (buff != null)
            {
                buff.CurrentDuration = 0;
            }
            else
            {
                e.Buff.Add(new DamageBuff
                {
                    Type = buffType,
                    Damage = Damage,
                    Interval = Interval,
                    CurrentInterval = Interval,
                    Duration = Duration
                });
            }
        }
    }
}
